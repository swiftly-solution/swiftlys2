using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Reflection;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Plugins;

namespace SwiftlyS2.Core.Services;

internal enum ProfilerLevel
{
    Disabled = 0,
    EventPipe = 1
}

[EventSource(Name = "SwiftlyS2-Profiler")]
internal sealed class ProfilerEventSource : EventSource
{
    public static readonly ProfilerEventSource Log = new();

    [Event(1, Level = EventLevel.Informational)]
    public void RecordingStart( string name ) => WriteEvent(1, name);

    [Event(2, Level = EventLevel.Informational)]
    public void RecordingStop( string name, double durationMs ) => WriteEvent(2, name, durationMs);

    [Event(3, Level = EventLevel.Informational)]
    public void RecordTime( string name, double durationMs ) => WriteEvent(3, name, durationMs);
}

internal class ProfileService
{
    private static readonly List<EventPipeProvider> s_providers = [
        // GC | Fusion | Loader | Jit | NGen | StopEnumeration | Security | AppDomainResourceManagement | Contention | Exception | Threading
        // JittedMethodILToNativeMap | OverrideAndSuppressNGen | Type | GCHeapSurvivalAndMovement | GCHeapAndTypeNames | Stack | ThreadTransfer | CodeSymbols
        new EventPipeProvider("Microsoft-Windows-DotNETRuntime", EventLevel.Verbose, 0x4c14fccbd),
        new EventPipeProvider("SwiftlyS2-Profiler", EventLevel.Informational),
        new EventPipeProvider("Microsoft-DotNETCore-SampleProfiler", EventLevel.Informational, 0)
    ];

    private readonly DiagnosticsClient _diagnosticsClient;
    private volatile ProfilerLevel _level = ProfilerLevel.Disabled;

    private readonly record struct RecordedEntry( string Name, double DurationMs, long TimestampUtcMs );

    private readonly ConcurrentDictionary<string, long> _activeRecordings = new();

    private EventPipeSession? _session;
    private CancellationTokenSource? _sessionCts;
    private Task? _drainTask;
    private string? _tempTraceFile;

    public ProfileService( PluginManager pluginManager )
    {
        _diagnosticsClient = new DiagnosticsClient(Environment.ProcessId);

        var defaultLevel = (ProfilerLevel)Math.Clamp(NativeCore.GetProfilerLevelByDefault(), 0, 1);
        if (defaultLevel != ProfilerLevel.Disabled)
            Enable(defaultLevel);
    }

    public void Enable( ProfilerLevel level )
    {
        if (_level == level) return;
        Disable();
        _level = level;

        if (level == ProfilerLevel.EventPipe)
            StartSession();
    }

    public void Disable()
    {
        if (_level == ProfilerLevel.Disabled) return;

        if (_level == ProfilerLevel.EventPipe)
            StopSession();

        _level = ProfilerLevel.Disabled;
    }

    public bool IsEnabled() => _level != ProfilerLevel.Disabled;

    public ProfilerLevel CurrentLevel => _level;

    public void StartRecordingWithIdentifier( string identifier, string name )
    {
        if (_level == ProfilerLevel.Disabled) return;
        var key = $"[{identifier}] {name}";
        _activeRecordings[key] = Stopwatch.GetTimestamp();
        ProfilerEventSource.Log.RecordingStart(key);
    }

    public void StopRecordingWithIdentifier( string identifier, string name )
    {
        if (_level == ProfilerLevel.Disabled) return;
        var key = $"[{identifier}] {name}";
        if (!_activeRecordings.TryRemove(key, out var startTs)) return;

        var durationMs = Stopwatch.GetElapsedTime(startTs).TotalMilliseconds;
        ProfilerEventSource.Log.RecordingStop(key, durationMs);
    }

    public void RecordTimeWithIdentifier( string identifier, string name, double duration )
    {
        if (_level == ProfilerLevel.Disabled) return;
        var key = $"[{identifier}] {name}";
        ProfilerEventSource.Log.RecordTime(key, duration);
    }

    public async Task SaveAsync( string rootDir, ILogger logger, bool generateSummary = true )
    {
        if (_session is null || _tempTraceFile is null)
        {
            logger.LogWarning("No active trace to save.");
            return;
        }

        logger.LogInformation("Saving profiler data...");
        await StopSessionAsync();

        string? savedPath = null;
        if (File.Exists(_tempTraceFile))
        {
            var dir = Path.Combine(rootDir, "profilers", Guid.NewGuid().ToString());
            _ = Directory.CreateDirectory(dir);
            savedPath = Path.Combine(dir, $"{DateTime.UtcNow:yyyyMMdd_HHmmss}.nettrace");
            File.Move(_tempTraceFile, savedPath);
        }

        if (savedPath is not null && generateSummary)
        {
            try
            {
                await Task.Run(() =>
                {
                    var hostDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                    var profilerDll = Path.Combine(hostDir, "SwiftlyS2.Profiler.dll");
                    if (!File.Exists(profilerDll)) return;
                    var asm = Assembly.LoadFrom(profilerDll);
                    _ = asm.GetType("SwiftlyS2.Core.Services.ProfilerAnalyzer")!
                       .GetMethod("Analyze")!
                       .Invoke(null, [savedPath, logger]);
                });
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Trace conversion failed, raw nettrace preserved.");
            }
        }

        logger.LogInformation("Profiler data saved to {FilePath}.", savedPath);

        if (_level == ProfilerLevel.EventPipe)
            StartSession();
    }

    private void StartSession()
    {
        try
        {
            _sessionCts = new CancellationTokenSource();
            _tempTraceFile = Path.GetTempFileName();
            _session = _diagnosticsClient.StartEventPipeSession(s_providers, circularBufferMB: 256);
            var stream = _session.EventStream;
            var destFile = _tempTraceFile;
            var ct = _sessionCts.Token;
            _drainTask = Task.Run(() => CopyToFileAsync(stream, destFile, ct), CancellationToken.None);
        }
        catch
        {
            _session = null;
            _tempTraceFile = null;
        }
    }

    private async Task StopSessionAsync()
    {
        var session = _session;
        if (session is null) return;

        var stopTask = Task.Run(() => { try { session.Stop(); } catch { } });

        if (_drainTask is not null)
            await Task.WhenAll(stopTask, _drainTask).ConfigureAwait(false);
        else
            await stopTask.ConfigureAwait(false);

        _sessionCts?.Cancel();
        session.Dispose();
        _session = null;
        _sessionCts = null;
        _drainTask = null;
    }

    private void StopSession()
    {
        var session = _session;
        if (session is null) return;

        var stopTask = Task.Run(() => { try { session.Stop(); } catch { } });
        Task.WhenAll(stopTask, _drainTask ?? Task.CompletedTask).Wait();

        _sessionCts?.Cancel();
        session.Dispose();
        _session = null;
        _sessionCts = null;
        _drainTask = null;
    }

    private static async Task CopyToFileAsync( Stream source, string destPath, CancellationToken ct )
    {
        try
        {
            await using var file = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, 65536, useAsync: true);
            await source.CopyToAsync(file, ct).ConfigureAwait(false);
        }
        catch (OperationCanceledException) { }
        catch { }
    }
}
