using System.Reflection;
using System.Runtime.Loader;
using System.Collections.Concurrent;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using SwiftlyS2.Shared;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Core.Modules.Plugins;
using System.Runtime.InteropServices;
using Semver;

namespace SwiftlyS2.Core.Plugins;

internal class PluginManager : IPluginManager
{
    private const int DefaultReloadDebounceSeconds = 2;
    private const int WindowsDelayMs = 300;
    private const int LinuxDelayMs = 5000;
    private const int MaxFileAccessRetries = 10;
    private const int InitialFileAccessDelayMs = 50;

    private readonly IServiceProvider _rootProvider;
    private readonly RootDirService _rootDirService;
    private readonly DataDirectoryService _dataDirectoryService;
    private readonly ILogger<PluginManager> _logger;
    private readonly InterfaceManager _interfaceManager;
    private readonly List<Type> _sharedTypes;
    private readonly List<PluginContext> _plugins;
    private readonly ConcurrentDictionary<string, DateTime> _fileLastChange;
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _fileReloadTokens;
    private readonly ConcurrentDictionary<string, string> _pluginLoadErrors;
    private readonly ConcurrentDictionary<string, Assembly> _exportAssemblies;
    private readonly FileSystemWatcher? _fileWatcher;

    public PluginManager(
        IServiceProvider provider,
        ILogger<PluginManager> logger,
        RootDirService rootDirService,
        DataDirectoryService dataDirectoryService )
    {
        _rootProvider = provider;
        _rootDirService = rootDirService;
        _dataDirectoryService = dataDirectoryService;
        _logger = logger;
        _interfaceManager = new InterfaceManager();
        _sharedTypes = [];
        _plugins = [];
        _fileLastChange = new ConcurrentDictionary<string, DateTime>();
        _fileReloadTokens = new ConcurrentDictionary<string, CancellationTokenSource>();
        _pluginLoadErrors = new ConcurrentDictionary<string, string>();
        _exportAssemblies = new ConcurrentDictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);

        if (NativeServerHelpers.UseAutoHotReload())
        {
            _fileWatcher = InitializeFileWatcher();
        }

        ConfigureAssemblyResolver();
    }

    internal void Initialize()
    {
        LoadExports();

        if (!NativeCore.PluginManualLoadState())
        {
            LoadPlugins();
        }
        else
        {
            EnumeratePluginDirectories(_rootDirService.GetPluginsRoot(), pluginDir =>
            {
                var folderName = Path.GetFileName(pluginDir);
                var dllPath = Path.Combine(pluginDir, folderName + ".dll");
                var metadata = (PluginMetadata?)null;
                if (File.Exists(dllPath))
                {
                    metadata = ReadMetadataFromDll(dllPath);
                }
                if (metadata == null)
                {
                    metadata = new PluginMetadata { Id = folderName, Version = "0.0.0" };
                }
                var context = new PluginContext {
                    PluginDirectory = pluginDir,
                    Status = PluginStatus.Unloaded,
                    Metadata = metadata
                };
                _plugins.Add(context);
            });

            LoadPluginsInOrder(NativeCore.PluginLoadOrder());
        }
    }

    private FileSystemWatcher InitializeFileWatcher()
    {
        var watcher = new FileSystemWatcher {
            Path = _rootDirService.GetPluginsRoot(),
            Filter = "*.dll",
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };

        watcher.Changed += OnPluginFileChanged;
        watcher.Created += OnPluginFileCreated;

        return watcher;
    }

    private void ConfigureAssemblyResolver()
    {
        AppDomain.CurrentDomain.AssemblyResolve += ( sender, e ) =>
        {
            var assemblyName = new AssemblyName(e.Name).Name ?? string.Empty;

            if (assemblyName.Equals("SwiftlyS2.CS2", StringComparison.OrdinalIgnoreCase))
            {
                return Assembly.GetExecutingAssembly();
            }

            if (_exportAssemblies.TryGetValue(assemblyName.ToLower(), out var exportAssembly))
            {
                return exportAssembly;
            }

            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => assemblyName.Equals(a.GetName().Name, StringComparison.OrdinalIgnoreCase));
        };
    }

    public IReadOnlyList<PluginContext> GetPlugins() => _plugins.AsReadOnly();

    public bool LoadPlugin( string pluginId, bool silent = false )
        => LoadPluginById(pluginId, silent);

    public bool UnloadPlugin( string pluginId, bool silent = false )
        => UnloadPluginById(pluginId, silent);

    public bool ReloadPlugin( string pluginId, bool silent = false )
        => ReloadPluginById(pluginId, silent);

    public PluginStatus? GetPluginStatus( string pluginId )
        => FindPluginById(pluginId)?.Status;

    public PluginStatus? GetPluginStatusByDllName( string dllName )
    {
        var pluginDir = FindPluginDirectoryByDllName(dllName);
        return string.IsNullOrWhiteSpace(pluginDir)
            ? null
            : FindPluginByDirectory(pluginDir)?.Status;
    }

    public PluginMetadata? GetPluginMetadata( string pluginId )
        => FindPluginById(pluginId)?.Metadata;

    public string? GetPluginPath( string pluginId )
        => FindPluginById(pluginId)?.PluginDirectory;

    public Dictionary<string, string> GetPluginPaths()
        => _plugins
            .Where(p => p.Metadata != null && p.PluginDirectory != null)
            .ToDictionary(p => p.Metadata!.Id, p => p.PluginDirectory!);

    public Dictionary<string, PluginStatus> GetAllPluginStatuses()
        => _plugins
            .Where(p => p.Metadata != null)
            .ToDictionary(p => p.Metadata!.Id, p => p.Status ?? PluginStatus.Indeterminate);

    public Dictionary<string, PluginMetadata> GetAllPluginMetadata()
        => _plugins
            .Where(p => p.Metadata != null)
            .ToDictionary(p => p.Metadata!.Id, p => p.Metadata!);

    public IEnumerable<string> GetAllPlugins()
        => _plugins
            .Where(p => p.Metadata != null)
            .Select(p => p.Metadata!.Id);

    public Dictionary<string, string> GetPluginLoadErrors()
        => new Dictionary<string, string>(_pluginLoadErrors);

    public void RegenerateTranslations()
    {
        foreach (var plugin in _plugins.Where(p => p.Core != null))
        {
            plugin.Core!.TranslationService.CreateTranslationResource();
        }
    }

    public bool LoadPluginById( string id, bool silent = false )
    {
        var context = FindPluginById(id, excludeStatuses: [PluginStatus.Loading, PluginStatus.Loaded]);
        if (context == null || string.IsNullOrWhiteSpace(context.PluginDirectory))
        {
            if (!silent) _logger.LogWarning("Failed to load plugin by id: {Id}", id);
            return false;
        }

        return LoadPluginByDllName(Path.GetFileName(context.PluginDirectory), hotReload: false, silent);
    }

    public bool LoadPluginByDllName( string dllName, bool hotReload, bool silent = false )
    {
        var pluginDir = FindPluginDirectoryByDllName(dllName);
        if (string.IsNullOrWhiteSpace(pluginDir))
        {
            if (!silent) _logger.LogWarning("Failed to load plugin by name: {DllName}", dllName);
            return false;
        }

        var oldContext = FindPluginByDirectory(pluginDir, excludeStatuses: [PluginStatus.Loading, PluginStatus.Loaded]);
        PluginContext? newContext = null;

        try
        {
            if (oldContext != null && _plugins.Remove(oldContext))
            {
                newContext = LoadPluginInternal(pluginDir, hotReload, silent);
            }
            else if (oldContext == null)
            {
                newContext = LoadPluginInternal(pluginDir, hotReload, silent);
            }

            if (newContext?.Status == PluginStatus.Loaded)
            {
                if (!silent)
                {
                    _logger.LogInformation("Loaded plugin: {Id}", newContext.Metadata!.Id);
                }
                return true;
            }

            throw new InvalidOperationException($"Failed to load plugin from: {pluginDir}");
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e))
            {
                _logger.LogWarning(e, "Failed to load plugin by name: {Path}", pluginDir);
                var pluginName = Path.GetFileName(pluginDir);
                _pluginLoadErrors[pluginName] = e.ToString();
            }
            return false;
        }
        finally
        {
            RebuildSharedServices();
        }
    }

    private void LoadPluginsInOrder( string loadOrder )
    {
        var plugins = loadOrder.Split('\x01');
        foreach (var plugin in plugins)
        {
            _ = LoadPluginById(plugin, silent: false);
        }
        RebuildSharedServices();
        NotifyAllPluginsLoaded();
    }

    private void LoadPlugins()
    {
        EnumeratePluginDirectories(_rootDirService.GetPluginsRoot(), pluginDir =>
        {
            var displayPath = GetDisplayPath(pluginDir);
            var dllName = Path.GetFileName(pluginDir);
            var fullDisplayPath = Path.Join(displayPath, $"{dllName}.dll");

            _logger.LogInformation("Loading plugin: {Path}", fullDisplayPath);

            try
            {
                var context = LoadPluginInternal(pluginDir, hotReload: false, silent: false);
                if (context?.Status == PluginStatus.Loaded)
                {
                    LogPluginLoadSuccess(context, displayPath);
                }
                else
                {
                    _logger.LogWarning("Failed to load plugin: {Path}", fullDisplayPath);
                    _pluginLoadErrors[dllName] = "Plugin failed to load (status not loaded)";
                    context?.Status = PluginStatus.Error;
                }
            }
            catch (Exception e)
            {
                if (GlobalExceptionHandler.Handle(ref e))
                {
                    _logger.LogWarning(e, "Failed to load plugin: {Path}", fullDisplayPath);
                    _pluginLoadErrors[dllName] = e.ToString();
                }
            }
        });

        RebuildSharedServices();
        NotifyAllPluginsLoaded();
    }

    private PluginContext? LoadPluginInternal( string directory, bool hotReload, bool silent = false )
    {
        var existingContext = _plugins.FirstOrDefault(p =>
            p.PluginDirectory?.Trim().Equals(directory.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);

        if (existingContext != null)
        {
            _ = _plugins.Remove(existingContext);
        }

        var context = new PluginContext {
            PluginDirectory = directory,
            Status = PluginStatus.Loading
        };
        _plugins.Add(context);

        var entrypointDll = GetPluginEntrypointPath(directory);
        if (!File.Exists(entrypointDll))
        {
            return FailWithError(context, silent, $"Plugin entrypoint DLL not found: {entrypointDll}");
        }

        var loader = CreatePluginLoader(entrypointDll);
        var pluginType = FindPluginType(loader);
        if (pluginType == null)
        {
            return FailWithError(context, silent, $"No plugin type found in: {entrypointDll}");
        }

        var metadata = pluginType.GetCustomAttribute<PluginMetadata>();
        if (metadata == null)
        {
            return FailWithError(context, silent, $"Plugin metadata not found in: {entrypointDll}");
        }

        context.Metadata = metadata;

        var coreVersion = NativeEngineHelpers.GetNativeVersion();
        var minimumApiVersion = context.Metadata.MinimumAPIVersion ?? "0.0.0";

        SemVersion? coreSemver = null;
        try
        {
            coreSemver = SemVersion.Parse(coreVersion, SemVersionStyles.AllowV);
        }
        catch (Exception) { }

        if (coreSemver != null)
        {
            SemVersion? minApiSemver = null;
            try
            {
                minApiSemver = SemVersion.Parse(minimumApiVersion, SemVersionStyles.AllowV);
            }
            catch (Exception e)
            {
                if (GlobalExceptionHandler.Handle(ref e))
                {
                    _logger.LogWarning(e, "Failed to parse minimum API version for plugin {Id}: '{Version}'. Falling back to '0.0.0'.", context.Metadata.Id, minimumApiVersion);
                }
                minApiSemver = new SemVersion(0, 0, 0);
            }

            var r = SemVersion.CompareSortOrder(coreSemver, minApiSemver);

            if (r < 0)
            {
                return FailWithError(context, silent, $"Plugin API version '{minimumApiVersion}' is newer than the core version '{coreVersion}'.");
            }
        }

        _dataDirectoryService.EnsurePluginDataDirectory(metadata.Id);

        var core = CreateSwiftlyCore(metadata, pluginType, directory);
        var plugin = InstantiatePlugin(pluginType, core);

        try
        {
            plugin.Load(hotReload);
            context.Status = PluginStatus.Loaded;
            context.Core = core;
            context.Plugin = plugin;
            context.Loader = loader;

            var pluginName = Path.GetFileName(directory);
            _ = _pluginLoadErrors.TryRemove(pluginName, out _);

            return context;
        }
        catch (Exception e)
        {
            _ = GlobalExceptionHandler.Handle(ref e);
            CleanupFailedPlugin(plugin, loader, core);
            _logger.LogError(e, "Exception occurred while loading plugin: {PluginPath}", entrypointDll);
            var pluginName = Path.GetFileName(directory);
            _pluginLoadErrors[pluginName] = e.ToString();
            return FailWithError(context, silent, $"Failed to load plugin: {entrypointDll}");
        }
    }

    public bool UnloadPluginById( string id, bool silent = false, bool rebuild = true )
    {
        var context = FindPluginById(id, excludeStatuses: [PluginStatus.Unloaded]);
        if (context == null)
        {
            if (!silent) _logger.LogWarning("Plugin not found: {Id}", id);
            return false;
        }

        try
        {
            context.Dispose();
            _ = _plugins.Remove(context);
            return true;
        }
        catch (Exception ex)
        {
            if (!silent) _logger.LogWarning(ex, "Failed to unload plugin: {Id}", id);
            context.Status = PluginStatus.Indeterminate;
            return false;
        }
        finally
        {
            if (rebuild)
            {
                RebuildSharedServices();
            }
        }
    }

    public bool UnloadPluginByDllName( string dllName, bool silent = false, bool rebuild = true )
    {
        var pluginDir = FindPluginDirectoryByDllName(dllName);
        if (string.IsNullOrWhiteSpace(pluginDir))
        {
            if (!silent) _logger.LogWarning("Failed to find plugin by name: {DllName}", dllName);
            return false;
        }

        var context = FindPluginByDirectory(pluginDir, excludeStatuses: [PluginStatus.Unloaded]);
        if (context?.Metadata == null)
        {
            if (!silent) _logger.LogWarning("Failed to find plugin by name: {DllName}", dllName);
            return false;
        }

        return UnloadPluginById(context.Metadata.Id, silent, rebuild);
    }

    public bool ReloadPluginById( string id, bool silent = false )
    {
        _ = UnloadPluginById(id, silent, rebuild: false);
        return LoadPluginById(id, silent);
    }

    public bool ReloadPluginByDllName( string dllName, bool silent = false )
    {
        _ = UnloadPluginByDllName(dllName, silent, rebuild: false);
        return LoadPluginByDllName(dllName, hotReload: true, silent);
    }

    private void LoadExports()
    {
        try
        {
            var resolver = new DependencyResolver(_logger);
            resolver.AnalyzeDependencies(_rootDirService.GetPluginsRoot());

            _logger.LogInformation("{Graph}", resolver.GetDependencyGraphVisualization());

            var loadOrder = resolver.GetLoadOrder();
            _logger.LogInformation("Loading {Count} export assemblies in dependency order", loadOrder.Count);

            foreach (var exportFile in loadOrder)
            {
                LoadExportAssembly(exportFile);
            }

            _logger.LogInformation("Loaded {Count} shared types", _sharedTypes.Count);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("circular dependency", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError(ex, "Circular dependency detected in plugin exports, loading manually");
            LoadExportsManually(_rootDirService.GetPluginsRoot());
        }
        catch (Exception ex)
        {
            if (GlobalExceptionHandler.Handle(ref ex))
            {
                _logger.LogError(ex, "Failed to load exports");
            }
        }
    }

    private void LoadExportAssembly( string exportFile )
    {
        try
        {
            var assembly = Assembly.LoadFrom(exportFile);
            var name = assembly.GetName().Name ?? Path.GetFileName(exportFile);
            _exportAssemblies[name.ToLower()] = assembly;
            var exports = assembly.GetTypes();
            _logger.LogDebug("Loaded {Count} types from {Path}", exports.Length, Path.GetFileName(exportFile));
            _sharedTypes.AddRange(exports);
        }
        catch (Exception ex)
        {
            if (GlobalExceptionHandler.Handle(ref ex))
            {
                _logger.LogWarning(ex, "Failed to load export assembly: {Path}", exportFile);
            }
        }
    }

    private void LoadExportsManually( string startDirectory )
    {
        EnumeratePluginDirectories(startDirectory, pluginDir =>
        {
            var exportsPath = Path.Combine(pluginDir, "resources", "exports");
            if (!Directory.Exists(exportsPath))
            {
                return;
            }

            foreach (var exportFile in Directory.GetFiles(exportsPath, "*.dll"))
            {
                LoadExportAssembly(exportFile);
            }
        });
    }

    private void OnPluginFileChanged( object sender, FileSystemEventArgs e )
    {
        if (!ShouldProcessFileChange(e, out var directoryName))
        {
            return;
        }

        try
        {
            if (!ShouldDebounceReload(directoryName))
            {
                return;
            }

            CancelPreviousReload(directoryName);
            var cts = RegisterNewReload(directoryName);
            SchedulePluginReload(e.FullPath, directoryName, cts);
        }
        catch (Exception ex)
        {
            if (GlobalExceptionHandler.Handle(ref ex))
            {
                _logger.LogError(ex, "Failed to handle plugin change");
            }
        }
    }

    private void OnPluginFileCreated( object sender, FileSystemEventArgs e )
    {
        if (!ShouldProcessFileChange(e, out var directoryName))
        {
            return;
        }

        try
        {
            var pluginDirectory = Path.GetDirectoryName(e.FullPath) ?? string.Empty;
            if (FindPluginByDirectory(pluginDirectory) != null)
            {
                _logger.LogInformation("Plugin already loaded, skipping: {Name}", directoryName);
                return;
            }

            var cts = new CancellationTokenSource();
            SchedulePluginLoad(e.FullPath, pluginDirectory, directoryName, cts);
        }
        catch (Exception ex)
        {
            if (GlobalExceptionHandler.Handle(ref ex))
            {
                _logger.LogError(ex, "Failed to handle new plugin creation");
            }
        }
    }

    private bool ShouldProcessFileChange( FileSystemEventArgs e, out string directoryName )
    {
        directoryName = string.Empty;

        if (!NativeServerHelpers.UseAutoHotReload())
        {
            return false;
        }

        var pluginDirectory = Path.GetDirectoryName(e.FullPath) ?? string.Empty;
        directoryName = Path.GetFileName(pluginDirectory) ?? string.Empty;
        var fileName = Path.GetFileNameWithoutExtension(e.FullPath);

        return !string.IsNullOrWhiteSpace(directoryName) && fileName.Equals(directoryName);
    }

    private bool ShouldDebounceReload( string directoryName )
    {
        var lastChange = _fileLastChange.GetValueOrDefault(directoryName, DateTime.MinValue);
        var timeSinceLastChange = (DateTime.UtcNow - lastChange).TotalSeconds;

        if (timeSinceLastChange > DefaultReloadDebounceSeconds)
        {
            _ = _fileLastChange.AddOrUpdate(directoryName, DateTime.UtcNow, ( _, _ ) => DateTime.UtcNow);
            return true;
        }

        return false;
    }

    private void CancelPreviousReload( string directoryName )
    {
        if (_fileReloadTokens.TryRemove(directoryName, out var oldCts))
        {
            oldCts.Cancel();
            oldCts.Dispose();
        }
    }

    private CancellationTokenSource RegisterNewReload( string directoryName )
    {
        var cts = new CancellationTokenSource();
        _ = _fileReloadTokens.AddOrUpdate(directoryName, cts, ( _, _ ) => cts);
        return cts;
    }

    private void SchedulePluginReload( string filePath, string directoryName, CancellationTokenSource cts )
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await WaitForPlatformDelay(cts.Token);
                await WaitForFileAccessibility(filePath, cts.Token);

                if (ReloadPluginByDllName(directoryName, silent: true))
                {
                    _logger.LogInformation("Reloaded plugin: {Name}", directoryName);
                }
                else
                {
                    _logger.LogWarning("Failed to reload plugin: {Name}", directoryName);
                }
            }
            catch (Exception) { }
        }, cts.Token);
    }

    private void SchedulePluginLoad( string filePath, string pluginDirectory, string directoryName, CancellationTokenSource cts )
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await WaitForPlatformDelay(cts.Token);
                await WaitForFileAccessibility(filePath, cts.Token);

                var context = LoadPluginInternal(pluginDirectory, hotReload: true, silent: false);
                if (context?.Status == PluginStatus.Loaded)
                {
                    var displayPath = GetDisplayPath(pluginDirectory);
                    _logger.LogInformation(
                        string.Join("\n", [
                            "Hot-loaded New Plugin",
                            "├─  {Id} {Version}",
                            "├─  Author: {Author}",
                            "└─  Path: {RelativePath}"
                        ]),
                        context.Metadata!.Id,
                        context.Metadata!.Version,
                        context.Metadata!.Author,
                        displayPath
                    );
                }
                else
                {
                    _logger.LogWarning("Failed to hot-load new plugin: {Name}", directoryName);
                }
            }
            catch (Exception ex)
            {
                if (GlobalExceptionHandler.Handle(ref ex))
                {
                    AnsiConsole.WriteException(ex);
                }
                _logger.LogError(ex, "Failed to hot-load new plugin: {Name}", directoryName);
            }
            finally
            {
                cts.Dispose();
            }
        }, cts.Token);
    }

    private static async Task WaitForPlatformDelay( CancellationToken token )
    {
        var delay = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? LinuxDelayMs : WindowsDelayMs;
        await Task.Delay(delay, token);
    }

    private async Task WaitForFileAccessibility( string filePath, CancellationToken token )
    {
        await WaitForFileAccess(filePath, token);

        var pdbFile = Path.ChangeExtension(filePath, ".pdb");
        if (File.Exists(pdbFile))
        {
            await WaitForFileAccess(pdbFile, token);
        }
    }

    private static async Task WaitForFileAccess( string filePath, CancellationToken token, int maxRetries = MaxFileAccessRetries, int initialDelayMs = InitialFileAccessDelayMs )
    {
        for (var i = 1; i <= maxRetries && !token.IsCancellationRequested; i++)
        {
            try
            {
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                return;
            }
            catch (IOException)
            {
                if (i < maxRetries)
                {
                    var delay = initialDelayMs * (1 << (i - 1)); // Exponential backoff
                    await Task.Delay(delay, token);
                }
            }
            catch
            {
                return;
            }
        }
    }

    public string? FindPluginDirectoryByDllName( string dllName )
    {
        dllName = dllName.Trim().TrimEnd(".dll", StringComparison.OrdinalIgnoreCase);

        var pluginDir = _plugins
            .FirstOrDefault(p => Path.GetFileName(p.PluginDirectory)?.Trim().Equals(dllName, StringComparison.OrdinalIgnoreCase) ?? false)
            ?.PluginDirectory;

        if (!string.IsNullOrWhiteSpace(pluginDir))
        {
            return pluginDir;
        }

        string? foundDir = null;
        EnumeratePluginDirectories(_rootDirService.GetPluginsRoot(), dir =>
        {
            if (Path.GetFileName(dir).Equals(dllName, StringComparison.OrdinalIgnoreCase))
            {
                foundDir = dir;
            }
        });

        return foundDir;
    }

    private PluginContext? FindPluginById( string id, PluginStatus[]? excludeStatuses = null )
    {
        var query = _plugins.AsEnumerable();

        if (excludeStatuses != null && excludeStatuses.Length > 0)
        {
            query = query.Where(p => !excludeStatuses.Contains(p.Status ?? PluginStatus.Indeterminate));
        }

        return query.FirstOrDefault(p =>
            (p.Metadata?.Id.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase) ?? false)
            || (p.PluginDirectory != null && Path.GetFileName(p.PluginDirectory).Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase)));
    }

    private PluginContext? FindPluginByDirectory( string directory, PluginStatus[]? excludeStatuses = null )
    {
        var query = _plugins.AsEnumerable();

        if (excludeStatuses != null && excludeStatuses.Length > 0)
        {
            query = query.Where(p => !excludeStatuses.Contains(p.Status ?? PluginStatus.Indeterminate));
        }

        return query.FirstOrDefault(p =>
            p.PluginDirectory?.Trim().Equals(directory.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);
    }

    private PluginLoader CreatePluginLoader( string entrypointDll )
    {
        var currentContext = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());

        return PluginLoader.CreateFromAssemblyFile(
            entrypointDll,
            [typeof(BasePlugin), .. _sharedTypes],
            config =>
            {
                config.IsUnloadable = true;
                config.LoadInMemory = true;

                if (currentContext != null)
                {
                    config.DefaultContext = currentContext;
                    config.PreferSharedTypes = true;
                }
            });
    }

    private static Type? FindPluginType( PluginLoader loader )
    {
        return loader.LoadDefaultAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.IsSubclassOf(typeof(BasePlugin)));
    }

    private SwiftlyCore CreateSwiftlyCore( PluginMetadata metadata, Type pluginType, string pluginDirectory )
    {
        var dataDir = _dataDirectoryService.GetPluginDataDirectory(metadata.Id);
        var core = new SwiftlyCore(
            metadata.Id,
            pluginDirectory,
            metadata,
            pluginType,
            _rootProvider,
            dataDir);

        core.InitializeType(pluginType);
        return core;
    }

    private static BasePlugin InstantiatePlugin( Type pluginType, SwiftlyCore core )
    {
        var plugin = (BasePlugin)Activator.CreateInstance(pluginType, [core])!;
        core.InitializeObject(plugin);
        return plugin;
    }

    private void CleanupFailedPlugin( BasePlugin? plugin, PluginLoader? loader, SwiftlyCore? core )
    {
        try
        {
            plugin?.Unload();
            loader?.Dispose();
            core?.Dispose();
        }
        catch (Exception ex)
        {
            if (GlobalExceptionHandler.Handle(ref ex))
            {
                AnsiConsole.WriteException(ex);
            }
        }
    }

    private PluginContext? FailWithError( PluginContext context, bool silent, string message )
    {
        if (!silent) _logger.LogWarning("{Message}", message);
        context.Status = PluginStatus.Error;

        if (!string.IsNullOrWhiteSpace(context.PluginDirectory))
        {
            var pluginName = Path.GetFileName(context.PluginDirectory);
            _pluginLoadErrors[pluginName] = message;
        }

        return null;
    }

    private void RebuildSharedServices()
    {
        _interfaceManager.Dispose();

        var loadedPlugins = _plugins
            .Where(p => p.Status == PluginStatus.Loaded && p.Plugin != null)
            .ToList();

        foreach (var plugin in loadedPlugins)
        {
            plugin.Plugin!.ConfigureSharedInterface(_interfaceManager);
        }

        _interfaceManager.Build();

        foreach (var plugin in loadedPlugins)
        {
            plugin.Plugin!.UseSharedInterface(_interfaceManager);
        }

        foreach (var plugin in loadedPlugins)
        {
            plugin.Plugin!.OnSharedInterfaceInjected(_interfaceManager);
        }
    }

    private void NotifyAllPluginsLoaded()
    {
        foreach (var plugin in _plugins.Where(p => p.Status == PluginStatus.Loaded && p.Plugin != null))
        {
            plugin.Plugin!.OnAllPluginsLoaded();
        }
    }

    private static void EnumeratePluginDirectories( string directory, Action<string> action )
    {
        foreach (var pluginDir in Directory.GetDirectories(directory))
        {
            var dirName = Path.GetFileName(pluginDir);

            // Handle nested plugin directories (e.g., [category])
            if (dirName.Trim().StartsWith('[') && dirName.EndsWith(']'))
            {
                EnumeratePluginDirectories(pluginDir, action);
                continue;
            }

            // Skip disabled directories
            if (IsDisabledDirectory(dirName))
            {
                continue;
            }

            action(pluginDir);
        }
    }

    private static bool IsDisabledDirectory( string dirName )
    {
        var trimmed = dirName.Trim();

        return trimmed.Equals("disable", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("disabled", StringComparison.OrdinalIgnoreCase) ||
               trimmed.Equals("_", StringComparison.OrdinalIgnoreCase) ||
               (trimmed.Length >= 2 && trimmed.StartsWith('_'));
    }

    private string GetDisplayPath( string pluginDirectory )
    {
        var relativePath = Path.GetRelativePath(_rootDirService.GetRoot(), pluginDirectory);
        return Path.Join("(swRoot)", relativePath);
    }

    private static string GetPluginEntrypointPath( string directory )
    {
        var dirName = Path.GetFileName(directory);
        return Path.Combine(directory, $"{dirName}.dll");
    }

    private void LogPluginLoadSuccess( PluginContext context, string displayPath )
    {
        _logger.LogInformation(
            string.Join("\n", [
                "Loaded Plugin",
                "├─  {Id} {Version}",
                "├─  Author: {Author}",
                "└─  Path: {RelativePath}"
            ]),
            context.Metadata!.Id,
            context.Metadata!.Version,
            context.Metadata!.Author,
            displayPath);
    }

    private PluginMetadata? ReadMetadataFromDll( string dllPath )
    {
        var tempLoader = new AssemblyLoadContext("TempMetadataLoader", isCollectible: true);
        try
        {
            var assembly = tempLoader.LoadFromAssemblyPath(dllPath);
            var types = new List<Type>();
            try
            {
                types.AddRange(assembly.GetTypes());
            }
            catch (ReflectionTypeLoadException ex)
            {
                types.AddRange(ex.Types.Where(t => t != null)!);
            }

            var pluginType = types.FirstOrDefault(t => t.IsSubclassOf(typeof(BasePlugin)));
            if (pluginType != null)
            {
                var metadata = pluginType.GetCustomAttribute<PluginMetadata>();
                if (metadata != null)
                {
                    return new PluginMetadata {
                        Id = metadata.Id,
                        Version = metadata.Version,
                        Author = metadata.Author,
                        Description = metadata.Description,
                        Website = metadata.Website,
                        Name = metadata.Name
                    };
                }
            }
        }
        catch
        {
        }
        finally
        {
            tempLoader.Unload();
        }
        return null;
    }
}

internal static class StringExtensions
{
    public static string TrimEnd( this string source, string suffix, StringComparison comparison )
    {
        return source.EndsWith(suffix, comparison)
            ? source[..^suffix.Length]
            : source;
    }
}