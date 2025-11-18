using System.Reflection;
using System.Runtime.Loader;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Modules.Plugins;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace SwiftlyS2.Core.Plugins;

internal class PluginManager
{
    private IServiceProvider _Provider { get; init; }
    private RootDirService _RootDirService { get; init; }
    private ILogger _Logger { get; init; }
    private List<PluginContext> _Plugins { get; } = [];
    private FileSystemWatcher? _Watcher { get; set; }
    private InterfaceManager _InterfaceManager { get; set; } = new();
    private List<Type> _SharedTypes { get; set; } = [];
    private DataDirectoryService _DataDirectoryService { get; init; }
    private DateTime lastRead = DateTime.MinValue;
    private readonly HashSet<string> reloadingPlugins = [];

    public PluginManager(
      IServiceProvider provider,
      ILogger<PluginManager> logger,
      RootDirService rootDirService,
      DataDirectoryService dataDirectoryService
    )
    {
        _Provider = provider;
        _RootDirService = rootDirService;
        _Logger = logger;
        _DataDirectoryService = dataDirectoryService;
        _Watcher = new FileSystemWatcher {
            Path = rootDirService.GetPluginsRoot(),
            Filter = "*.dll",
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite,
        };

        _Watcher.Changed += HandlePluginChange;

        _Watcher.EnableRaisingEvents = true;

        Initialize();
    }

    public void Initialize()
    {
        AppDomain.CurrentDomain.AssemblyResolve += ( sender, e ) =>
        {
            var loadingAssemblyName = new AssemblyName(e.Name).Name ?? "";
            if (string.IsNullOrWhiteSpace(loadingAssemblyName))
            {
                return null;
            }

            if (loadingAssemblyName == "SwiftlyS2.CS2")
            {
                return Assembly.GetExecutingAssembly();
            }

            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => loadingAssemblyName == a.GetName().Name);

            return loadedAssembly ?? null;
        };
        LoadExports();
        LoadPlugins();
    }

    public void HandlePluginChange( object sender, FileSystemEventArgs e )
    {
        try
        {
            if (!NativeServerHelpers.UseAutoHotReload())
            {
                return;
            }

            // Windows FileSystemWatcher triggers multiple (open, write, close) events for a single file change
            if (DateTime.Now - lastRead < TimeSpan.FromSeconds(1))
            {
                return;
            }

            var directory = Path.GetDirectoryName(e.FullPath);
            if (directory == null)
            {
                return;
            }

            foreach (var plugin in _Plugins)
            {
                if (Path.GetFileName(plugin?.PluginDirectory) == Path.GetFileName(directory))
                {
                    var pluginId = plugin?.Metadata?.Id;
                    if (string.IsNullOrWhiteSpace(pluginId))
                    {
                        break;
                    }

                    lock (reloadingPlugins)
                    {
                        if (reloadingPlugins.Contains(pluginId))
                        {
                            return;
                        }
                        _ = reloadingPlugins.Add(pluginId);
                    }

                    lastRead = DateTime.Now;

                    // meh, Idk why, but when using Mstsc to copy and overwrite files
                    // it sometimes triggers: "System.IO.IOException: The process cannot access the file because it is being used by another process."
                    // therefore, we use a retry mechanism
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await Task.Delay(500);

                            var fileLockSuccess = false;
                            for (var attempt = 0; attempt < 3; attempt++)
                            {
                                try
                                {
                                    using (var stream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.None))
                                    {
                                    }
                                    fileLockSuccess = true;
                                    break;
                                }
                                catch (IOException) when (attempt < 1)
                                {
                                    _Logger.LogWarning($"{Path.GetFileName(plugin?.PluginDirectory)} is locked, retrying in 500ms... (Attempt {attempt + 1}/3)");
                                    await Task.Delay(500);
                                }
                                catch (IOException)
                                {
                                    _Logger.LogError($"Failed to reload {Path.GetFileName(plugin?.PluginDirectory)} after 3 attempts");
                                }
                            }

                            if (fileLockSuccess)
                            {
                                ReloadPlugin(pluginId);
                            }
                        }
                        finally
                        {
                            lock (reloadingPlugins)
                            {
                                _ = reloadingPlugins.Remove(pluginId);
                            }
                        }
                    });

                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (!GlobalExceptionHandler.Handle(ex)) return;
            _Logger.LogError(ex, "Error handling plugin change");
        }
    }

    private void PopulateSharedManually( string startDirectory )
    {
        var pluginDirs = Directory.GetDirectories(startDirectory);

        foreach (var pluginDir in pluginDirs)
        {
            var dirName = Path.GetFileName(pluginDir);
            if (dirName.StartsWith("[") && dirName.EndsWith("]")) PopulateSharedManually(pluginDir);
            else
            {
                if (Directory.Exists(Path.Combine(pluginDir, "resources", "exports")))
                {
                    var exportFiles = Directory.GetFiles(Path.Combine(pluginDir, "resources", "exports"), "*.dll");
                    foreach (var exportFile in exportFiles)
                    {
                        try
                        {
                            var assembly = Assembly.LoadFrom(exportFile);
                            var exports = assembly.GetTypes();
                            foreach (var export in exports)
                            {
                                _SharedTypes.Add(export);
                            }
                        }
                        catch (Exception innerEx)
                        {
                            if (!GlobalExceptionHandler.Handle(innerEx)) return;
                            _Logger.LogWarning(innerEx, $"Failed to load export assembly: {exportFile}");
                        }
                    }
                }
            }
        }
    }

    private void LoadExports()
    {
        var resolver = new DependencyResolver(_Logger);

        try
        {
            resolver.AnalyzeDependencies(_RootDirService.GetPluginsRoot());

            _Logger.LogInformation(resolver.GetDependencyGraphVisualization());

            var loadOrder = resolver.GetLoadOrder();

            _Logger.LogInformation($"Loading {loadOrder.Count} export assemblies in dependency order.");

            foreach (var exportFile in loadOrder)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(exportFile);
                    var exports = assembly.GetTypes();

                    _Logger.LogDebug($"Loaded {exports.Length} types from {Path.GetFileName(exportFile)}.");


                    foreach (var export in exports)
                    {
                        _SharedTypes.Add(export);
                    }
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex)) return;
                    _Logger.LogWarning(ex, $"Failed to load export assembly: {exportFile}");
                }
            }

            _Logger.LogInformation($"Successfully loaded {_SharedTypes.Count} shared types.");
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Circular dependency"))
        {
            _Logger.LogError(ex, "Circular dependency detected in plugin exports. Loading exports without dependency resolution.");
            PopulateSharedManually(_RootDirService.GetPluginsRoot());
        }
        catch (Exception ex)
        {
            if (!GlobalExceptionHandler.Handle(ex)) return;
            _Logger.LogError(ex, "Unexpected error during export loading");
        }
    }

    private void LoadPluginsFromFolder( string directory )
    {
        var pluginDirs = Directory.GetDirectories(directory);

        foreach (var pluginDir in pluginDirs)
        {
            var dirName = Path.GetFileName(pluginDir);
            if (dirName.StartsWith("[") && dirName.EndsWith("]")) LoadPluginsFromFolder(pluginDir);
            else
            {
                try
                {
                    var context = LoadPlugin(pluginDir, false);
                    if (context != null && context.Status == PluginStatus.Loaded)
                    {
                        _Logger.LogInformation("Loaded plugin " + context.Metadata!.Id);
                    }
                }
                catch (Exception e)
                {
                    if (!GlobalExceptionHandler.Handle(e)) continue;
                    _Logger.LogWarning(e, "Error loading plugin: " + pluginDir);
                    continue;
                }
            }
        }
    }

    private void LoadPlugins()
    {
        LoadPluginsFromFolder(_RootDirService.GetPluginsRoot());

        RebuildSharedServices();

        _Plugins
          .Where(p => p.Status == PluginStatus.Loaded)
          .ToList()
          .ForEach(p => p.Plugin!.OnAllPluginsLoaded());
    }

    public List<PluginContext> GetPlugins()
    {
        return _Plugins;
    }

    private void RebuildSharedServices()
    {
        _InterfaceManager.Dispose();

        _Plugins
          .Where(p => p.Status == PluginStatus.Loaded)
          .ToList()
          .ForEach(p => p.Plugin!.ConfigureSharedInterface(_InterfaceManager));


        _InterfaceManager.Build();

        _Plugins
          .Where(p => p.Status == PluginStatus.Loaded)
          .ToList()
          .ForEach(p => p.Plugin!.UseSharedInterface(_InterfaceManager));

        _Plugins
            .Where(p => p.Status == PluginStatus.Loaded)
            .ToList()
            .ForEach(p => p.Plugin!.OnSharedInterfaceInjected(_InterfaceManager));
    }


    public PluginContext? LoadPlugin( string dir, bool hotReload )
    {


        PluginContext context = new() {
            PluginDirectory = dir,
            Status = PluginStatus.Loading,
        };
        _Plugins.Add(context);

        var entrypointDll = Path.Combine(dir, Path.GetFileName(dir) + ".dll");

        if (!File.Exists(entrypointDll))
        {
            _Logger.LogWarning("Plugin entrypoint DLL not found: " + entrypointDll);
            context.Status = PluginStatus.Error;
            return null;
        }

        var loader = PluginLoader.CreateFromAssemblyFile(
            assemblyFile: entrypointDll,
            sharedTypes: [typeof(BasePlugin), .. _SharedTypes],
            config =>
            {
                config.IsUnloadable = true;
                config.LoadInMemory = true;
                var currentContext = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());
                if (currentContext != null)
                {
                    config.DefaultContext = currentContext;
                    config.PreferSharedTypes = true;
                }
            }
        );

        var assembly = loader.LoadDefaultAssembly();

        var pluginType = assembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(BasePlugin)))!;

        if (pluginType == null)
        {
            _Logger.LogWarning("Plugin type not found: " + entrypointDll);
            context.Status = PluginStatus.Error;
            return null;
        }

        var metadata = pluginType.GetCustomAttribute<PluginMetadata>();
        if (metadata == null)
        {
            _Logger.LogWarning("Plugin metadata not found: " + entrypointDll);
            context.Status = PluginStatus.Error;
            return null;
        }

        context.Metadata = metadata;

        _DataDirectoryService.EnsurePluginDataDirectory(metadata.Id);

        var core = new SwiftlyCore(metadata.Id, Path.GetDirectoryName(entrypointDll)!, metadata, pluginType, _Provider, _DataDirectoryService.GetPluginDataDirectory(metadata.Id));

        core.InitializeType(pluginType);

        var plugin = (BasePlugin)Activator.CreateInstance(pluginType, [core])!;

        core.InitializeObject(plugin);


        try
        {
            plugin.Load(hotReload);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                context.Status = PluginStatus.Error;
                return null;
            }
            _Logger.LogWarning(e, $"Error loading plugin {entrypointDll}");
            context.Status = PluginStatus.Error;
            return null;
        }

        context.Status = PluginStatus.Loaded;
        context.Core = core;
        context.Plugin = plugin;
        context.Loader = loader;

        return context;
    }

    public bool UnloadPlugin( string id )
    {
        var context = _Plugins
          .Where(p => p.Status == PluginStatus.Loaded)
          .FirstOrDefault(p => p.Metadata?.Id == id);
        if (context == null)
        {
            _Logger.LogWarning("Plugin not found or not loaded: " + id);
            return false;
        }

        context.Dispose();
        context.Status = PluginStatus.Unloaded;
        return true;
    }

    public bool LoadPluginById( string id )
    {
        var context = _Plugins
          .Where(p => p.Status == PluginStatus.Unloaded)
          .FirstOrDefault(p => p.Metadata?.Id == id);
        if (context == null)
        {
            // try to find new plugins
            var root = _RootDirService.GetPluginsRoot();
            var pluginDirs = Directory.GetDirectories(root);
            foreach (var pluginDir in pluginDirs)
            {
                if (Path.GetFileName(pluginDir) == id)
                {
                    context = LoadPlugin(pluginDir, false);
                    break;
                }
            }
            if (context == null)
            {
                _Logger.LogWarning("Plugin not found: " + id);
                return false;
            }
        }
        else
        {
            var directory = context.PluginDirectory!;
            _ = _Plugins.Remove(context);
            _ = LoadPlugin(directory, true);
        }

        RebuildSharedServices();
        return true;
    }

    public void ReloadPlugin( string id )
    {
        _Logger.LogInformation("Reloading plugin " + id);

        if (!UnloadPlugin(id))
        {
            return;
        }

        if (!LoadPluginById(id))
        {
            RebuildSharedServices();
        }

        _Logger.LogInformation("Reloaded plugin " + id);
    }
}