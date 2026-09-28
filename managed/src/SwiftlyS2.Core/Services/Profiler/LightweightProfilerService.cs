using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Plugins;

namespace SwiftlyS2.Core.Services.Profiler;

internal sealed class LightweightProfilerService
{
    internal static LightweightProfilerService? Current;

    private static readonly string[] SelfExcludedNamespaces = [
        "HarmonyLib",
        "MonoMod",
        "System",
        "SwiftlyS2.Core.Services.Profiler",
        "SwiftlyS2.Shared.SteamAPI",
        "SwiftlyS2.Core.SchemaDefinitions",
        "SwiftlyS2.Shared.SchemaDefinitions",
        "SwiftlyS2.Core.ProtobufDefinitions",
        "SwiftlyS2.Core.GameEventDefinitions",
        "SwiftlyS2.Shared.ProtobufDefinitions",
        "SwiftlyS2.Shared.GameEventDefinitions",
        "SwiftlyS2.Core.GameHooks",
        "SwiftlyS2.Core.Natives",
    ];

    private const string GlobalHarmonyId = "swiftlys2.profiler.global";
    private const string PluginHarmonyIdPrefix = "swiftlys2.profiler.plugin.";

    private readonly PluginManager _pluginManager;
    private readonly ILogger _logger;

    private readonly LightweightAggregator _methodAggregator = new();

    private readonly LightweightAggregator _customAggregator = new();

    private readonly ConcurrentDictionary<Assembly, string> _assemblyIdentifiers = new();

    private readonly ConcurrentDictionary<Assembly, MethodInfo[]> _discoveredMethods = new();

    private readonly Dictionary<string, Harmony> _pluginHarmonyInstances = new();
    private readonly Task _startupDiscoveryTask;

    private Harmony? _globalHarmony;
    private volatile bool _enabled;

    public LightweightProfilerService( PluginManager pluginManager, ILogger<LightweightProfilerService> logger )
    {
        _pluginManager = pluginManager;
        _logger = logger;

        AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        _pluginManager.PluginLoaded += OnPluginLoaded;
        _pluginManager.PluginUnloading += OnPluginUnloading;

        foreach (var context in _pluginManager.GetPlugins())
            DiscoverPlugin(context);

        _startupDiscoveryTask = Task.Run(() =>
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                DiscoverFrameworkAssembly(asm);
        });
    }

    public bool IsEnabled => _enabled;

    public void Enable()
    {
        if (_enabled) return;
        _enabled = true;
        _methodAggregator.Reset();
        _customAggregator.Reset();
        Current = this;

        _startupDiscoveryTask.Wait();

        if (_globalHarmony is null)
        {
            _globalHarmony = new Harmony(GlobalHarmonyId);
            foreach (var (asm, identifier) in _assemblyIdentifiers)
            {
                if (identifier == "SwiftlyS2")
                    ApplyCachedPatches(_globalHarmony, asm, identifier);
            }
        }

        foreach (var context in _pluginManager.GetPlugins())
            PatchPlugin(context);

        _logger.LogWarning("Lightweight profiler enabled. This will add per-call overhead while active.");
    }
    public void Disable()
    {
        if (!_enabled) return;
        _enabled = false;
        Current = null;

        _globalHarmony?.UnpatchAll(GlobalHarmonyId);
        _globalHarmony = null;

        foreach (var harmony in _pluginHarmonyInstances.Values)
            harmony.UnpatchAll(harmony.Id);
        _pluginHarmonyInstances.Clear();
    }

    public LightweightSnapshot Snapshot() => new(
        _methodAggregator.SessionElapsedMs(),
        _methodAggregator.Snapshot(),
        _customAggregator.Snapshot());

    public void ResetWindow()
    {
        _methodAggregator.Reset();
        _customAggregator.Reset();
    }

    public void RecordManual( string identifier, string operation, double durationMs )
    {
        if (!_enabled) return;
        _customAggregator.Record(identifier, operation, durationMs);
    }

    private void OnAssemblyLoad( object? sender, AssemblyLoadEventArgs args )
    {
        DiscoverFrameworkAssembly(args.LoadedAssembly);

        if (_enabled && _globalHarmony is not null
            && _assemblyIdentifiers.TryGetValue(args.LoadedAssembly, out var identifier)
            && identifier == "SwiftlyS2")
        {
            ApplyCachedPatches(_globalHarmony, args.LoadedAssembly, identifier);
        }
    }

    private void OnPluginLoaded( PluginContext context )
    {
        DiscoverPlugin(context);
        if (_enabled) PatchPlugin(context);
    }

    private void OnPluginUnloading( PluginContext context )
    {
        var id = context.Metadata?.Id;
        if (id is not null && _pluginHarmonyInstances.Remove(id, out var harmony))
            harmony.UnpatchAll(harmony.Id);

        if (context.Loader is not null)
        {
            try
            {
                var asm = context.Loader.LoadDefaultAssembly();
                _ = _discoveredMethods.TryRemove(asm, out _);
                _ = _assemblyIdentifiers.TryRemove(asm, out _);
            }
            catch { }
        }
    }

    private void PatchPlugin( PluginContext context )
    {
        if (context.Metadata?.Id is not { } id || context.Loader is null) return;
        if (_pluginHarmonyInstances.ContainsKey(id)) return;

        Assembly asm;
        try { asm = context.Loader.LoadDefaultAssembly(); }
        catch { return; }

        var harmony = new Harmony($"{PluginHarmonyIdPrefix}{id}");
        _pluginHarmonyInstances[id] = harmony;
        ApplyCachedPatches(harmony, asm, id);
    }

    private void DiscoverPlugin( PluginContext context )
    {
        if (context.Metadata?.Id is not { } id || context.Loader is null) return;

        Assembly asm;
        try { asm = context.Loader.LoadDefaultAssembly(); }
        catch { return; }

        DiscoverAndCacheAssembly(asm, id);
    }

    private void DiscoverFrameworkAssembly( Assembly asm )
    {
        if (asm.IsDynamic) return;

        var alc = AssemblyLoadContext.GetLoadContext(asm);
        if (alc is { IsCollectible: true }) return;

        var name = asm.GetName().Name ?? "";
        if (!name.StartsWith("SwiftlyS2.", StringComparison.Ordinal)) return;

        DiscoverAndCacheAssembly(asm, "SwiftlyS2");
    }

    private void DiscoverAndCacheAssembly( Assembly asm, string identifier )
    {
        if (_discoveredMethods.ContainsKey(asm)) return;
        _assemblyIdentifiers[asm] = identifier;

        Type?[] types;
        try { types = asm.GetTypes(); }
        catch (ReflectionTypeLoadException ex) { types = ex.Types; }
        catch { _discoveredMethods[asm] = []; return; }

        var candidates = new List<MethodInfo>();

        foreach (var type in types)
        {
            if (type is null) continue;

            try
            {
                if (type.Namespace is { } ns && IsSelfExcluded(ns)) continue;
                if (IsSelfExcludedType(type)) continue;
                if (Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute))) continue;

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (var method in methods)
                    if (IsPatchable(method))
                        candidates.Add(method);
            }
            catch
            {
            }
        }

        _discoveredMethods[asm] = candidates.ToArray();
    }

    private void ApplyCachedPatches( Harmony harmony, Assembly asm, string identifier )
    {
        if (!_discoveredMethods.TryGetValue(asm, out var candidates))
        {
            DiscoverAndCacheAssembly(asm, identifier);
            if (!_discoveredMethods.TryGetValue(asm, out candidates)) return;
        }

        foreach (var method in candidates)
            PatchMethod(harmony, method, identifier);
    }

    private static bool IsSelfExcluded( string ns )
    {
        foreach (var baseNs in SelfExcludedNamespaces)
            if (ns == baseNs || (ns.Length > baseNs.Length && ns.StartsWith(baseNs, StringComparison.Ordinal) && ns[baseNs.Length] == '.'))
                return true;
        return false;
    }

    private static bool IsSelfExcludedType( Type type )
    {
        if (type == typeof(object) || type == typeof(Type) || type.FullName == "System.RuntimeType")
            return true;

        if (type.FullName is { } fullName && fullName.StartsWith("Interop", StringComparison.Ordinal))
            return true;

        return false;
    }

    private static bool IsPatchable( MethodInfo method )
    {
        try
        {
            if (method.IsAbstract) return false;
            if (method.ContainsGenericParameters) return false;
            if (method.IsGenericMethodDefinition) return false;
            if ((method.GetMethodImplementationFlags() & MethodImplAttributes.InternalCall) != 0) return false;
            if (method.Attributes.HasFlag(MethodAttributes.PinvokeImpl)) return false;
            if (Attribute.IsDefined(method, typeof(CompilerGeneratedAttribute))) return false;
            if (method.GetMethodBody() is null) return false;
            if (Attribute.IsDefined(method, typeof(UnmanagedCallersOnlyAttribute))) return false;
            if (IsUnsafePointerType(method.ReturnType)) return false;
            foreach (var p in method.GetParameters())
                if (IsUnsafePointerType(p.ParameterType)) return false;

            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsUnsafePointerType( Type type )
        => type.IsPointer || type.IsFunctionPointer || type.IsByRef;

    private void PatchMethod( Harmony harmony, MethodInfo method, string identifier )
    {
        try
        {
            var prefix = new HarmonyMethod(typeof(ProfilerPatches).GetMethod(nameof(ProfilerPatches.Prefix))!);
            var finalizer = method.ReturnType == typeof(void)
                ? new HarmonyMethod(typeof(ProfilerPatches).GetMethod(nameof(ProfilerPatches.FinalizerVoid))!)
                : new HarmonyMethod(typeof(ProfilerPatches).GetMethod(nameof(ProfilerPatches.FinalizerResult))!);

            _ = harmony.Patch(method, prefix: prefix, finalizer: finalizer);
        }
        catch
        {
        }
    }

    [ThreadStatic] private static bool _inRecordCompletion;

    internal void RecordCompletion( MethodBase originalMethod, long startTicks, object? result )
    {
        if (_inRecordCompletion) return;
        _inRecordCompletion = true;
        try
        {
            var task = ExtractTask(result);
            if (task is not null)
            {
                _ = task.ContinueWith(_ => RecordNow(originalMethod, startTicks), TaskScheduler.Default);
                return;
            }
            RecordNow(originalMethod, startTicks);
        }
        catch
        {
        }
        finally
        {
            _inRecordCompletion = false;
        }
    }

    private static Task? ExtractTask( object? result )
    {
        switch (result)
        {
            case null: return null;
            case Task t: return t;
            case ValueTask vt: return vt.AsTask();
        }

        var asTaskMethod = ProfilerPatches.GetValueTaskAsTask(result.GetType());
        return asTaskMethod?.Invoke(result, null) as Task;
    }

    private void RecordNow( MethodBase originalMethod, long startTicks )
    {
        if (!_enabled) return;
        var elapsedMs = Stopwatch.GetElapsedTime(startTicks).TotalMilliseconds;
        var declaringType = originalMethod.DeclaringType;
        var identifier = declaringType?.Assembly is { } asm && _assemblyIdentifiers.TryGetValue(asm, out var id) ? id : "Unknown";
        var name = $"{declaringType?.FullName}.{originalMethod.Name}";
        _methodAggregator.Record(identifier, name, elapsedMs);
    }
}

internal readonly record struct LightweightSnapshot(
    double SessionElapsedMs,
    Dictionary<string, Dictionary<string, LightweightRecordingSnapshot>> MethodGroups,
    Dictionary<string, Dictionary<string, LightweightRecordingSnapshot>> CustomGroups
);
