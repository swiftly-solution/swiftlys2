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

namespace SwiftlyS2.Core.Plugins;

internal class PluginManager
{
    private readonly IServiceProvider rootProvider;
    private readonly RootDirService rootDirService;
    private readonly DataDirectoryService dataDirectoryService;
    private readonly ILogger logger;

    private readonly InterfaceManager interfaceManager;
    private readonly List<Type> sharedTypes;
    private readonly List<PluginContext> plugins;
    private readonly ConcurrentDictionary<string, DateTime> fileLastChange;
    private readonly ConcurrentDictionary<string, CancellationTokenSource> fileReloadTokens;

    private readonly FileSystemWatcher? fileWatcher;

    public PluginManager(
        IServiceProvider provider,
        ILogger<PluginManager> logger,
        RootDirService rootDirService,
        DataDirectoryService dataDirectoryService
    )
    {
        this.rootProvider = provider;
        this.rootDirService = rootDirService;
        this.dataDirectoryService = dataDirectoryService;
        this.logger = logger;

        this.interfaceManager = new();
        this.sharedTypes = [];
        this.plugins = [];
        this.fileLastChange = new ConcurrentDictionary<string, DateTime>();
        this.fileReloadTokens = new ConcurrentDictionary<string, CancellationTokenSource>();

        this.fileWatcher = new FileSystemWatcher {
            Path = rootDirService.GetPluginsRoot(),
            Filter = "*.dll",
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };
        this.fileWatcher.Changed += ( sender, e ) =>
        {
            static async Task WaitForFileAccess( CancellationToken token, string filePath, int maxRetries = 10, int initialDelayMs = 50 )
            {
                for (var i = 1; i <= maxRetries && !token.IsCancellationRequested; i++)
                {
                    try
                    {
                        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        break;
                    }
                    catch (IOException)
                    {
                        if (i < maxRetries)
                        {
                            // 50ms, 100ms, 200ms, 400ms, 800ms...
                            await Task.Delay(initialDelayMs * (1 << (i - 1)), token);
                        }
                        continue;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }

            try
            {
                if (!NativeServerHelpers.UseAutoHotReload() || e.ChangeType != WatcherChangeTypes.Changed)
                {
                    return;
                }

                var directoryName = Path.GetFileName(Path.GetDirectoryName(e.FullPath)) ?? string.Empty;
                var fileName = Path.GetFileNameWithoutExtension(e.FullPath);
                if (string.IsNullOrWhiteSpace(directoryName) || !fileName.Equals(directoryName))
                {
                    return;
                }

                if ((DateTime.UtcNow - fileLastChange.GetValueOrDefault(directoryName, DateTime.MinValue)).TotalSeconds > 2)
                {
                    _ = fileLastChange.AddOrUpdate(directoryName, DateTime.UtcNow, ( _, _ ) => DateTime.UtcNow);

                    if (fileReloadTokens.TryRemove(directoryName, out var oldCts))
                    {
                        oldCts.Cancel();
                        oldCts.Dispose();
                    }

                    var cts = new CancellationTokenSource();
                    _ = fileReloadTokens.AddOrUpdate(directoryName, cts, ( _, _ ) => cts);

                    // Wait for file to be accessible, then reload
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await WaitForFileAccess(cts.Token, e.FullPath);
                            Console.WriteLine("\n");
                            if (ReloadPluginByDllName(directoryName, true))
                            {
                                logger.LogInformation("Reloaded plugin: {Format}", directoryName);
                            }
                            else
                            {
                                logger.LogWarning("Failed to reload plugin: {Format}", directoryName);
                            }
                            Console.WriteLine("\n");
                        }
                        catch (Exception ex)
                        {
                            if (GlobalExceptionHandler.Handle(ex))
                            {
                                AnsiConsole.WriteException(ex);
                            }
                        }
                    }, cts.Token);
                }
            }
            catch (Exception ex)
            {
                if (!GlobalExceptionHandler.Handle(ex))
                {
                    return;
                }
                logger.LogError(ex, "Failed to handle plugin change");
            }
        };

        AppDomain.CurrentDomain.AssemblyResolve += ( sender, e ) =>
        {
            var loadingAssemblyName = new AssemblyName(e.Name).Name ?? string.Empty;
            return loadingAssemblyName.Equals("SwiftlyS2.CS2", StringComparison.OrdinalIgnoreCase)
                ? Assembly.GetExecutingAssembly()
                : AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => loadingAssemblyName == a.GetName().Name);
        };

        LoadExports();
        LoadPlugins();
    }

    public IReadOnlyList<PluginContext> GetPlugins() => plugins.AsReadOnly();

    public string? FindPluginDirectoryByDllName( string dllName )
    {
        dllName = dllName.Trim();
        if (dllName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
        {
            dllName = dllName[..^4];
        }

        var pluginDir = plugins
            .FirstOrDefault(p => Path.GetFileName(p.PluginDirectory)?.Trim().Equals(dllName.Trim()) ?? false)
            ?.PluginDirectory;

        if (!string.IsNullOrWhiteSpace(pluginDir))
        {
            return pluginDir;
        }

        string? foundDir = null;
        EnumeratePluginDirectories(rootDirService.GetPluginsRoot(), dir =>
        {
            if (Path.GetFileName(dir).Equals(dllName))
            {
                foundDir = dir;
            }
        });

        return foundDir;
    }

    public bool UnloadPluginById( string id, bool silent = false )
    {
        var context = plugins
            .Where(p => p.Status != PluginStatus.Unloaded)
            .FirstOrDefault(p => p.Metadata?.Id.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);

        try
        {
            context?.Dispose();
            context?.Loader?.Dispose();
            context?.Core?.Dispose();
            context!.Status = PluginStatus.Unloaded;
            return true;
        }
        catch
        {
            if (!silent)
            {
                logger.LogWarning("Failed to unload plugin by id: {Id}", id);
            }
            if (context != null)
            {
                context.Status = PluginStatus.Indeterminate;
            }
            return false;
        }
        finally
        {
            RebuildSharedServices();
        }
    }

    public bool UnloadPluginByDllName( string dllName, bool silent = false )
    {
        var pluginDir = FindPluginDirectoryByDllName(dllName);
        if (string.IsNullOrWhiteSpace(pluginDir))
        {
            if (!silent)
            {
                logger.LogWarning("Failed to find plugin by name: {DllName}", dllName);
            }
            return false;
        }

        var context = plugins
            .Where(p => p.Status != PluginStatus.Unloaded)
            .FirstOrDefault(p => p.PluginDirectory?.Trim().Equals(pluginDir.Trim()) ?? false);

        if (string.IsNullOrWhiteSpace(context?.Metadata?.Id))
        {
            if (!silent)
            {
                logger.LogWarning("Failed to find plugin by name: {DllName}", dllName);
            }
            return false;
        }

        return UnloadPluginById(context.Metadata.Id, silent);
    }

    public bool LoadPluginById( string id, bool silent = false )
    {
        var context = plugins
            .Where(p => p.Status != PluginStatus.Loading && p.Status != PluginStatus.Loaded)
            .FirstOrDefault(p => p.Metadata?.Id.Trim().Equals(id.Trim(), StringComparison.OrdinalIgnoreCase) ?? false);

        if (string.IsNullOrWhiteSpace(context?.PluginDirectory))
        {
            if (!silent)
            {
                logger.LogWarning("Failed to load plugin by id: {Id}", id);
            }
            return false;
        }

        return LoadPluginByDllName(Path.GetFileName(context.PluginDirectory), silent);
    }

    public bool LoadPluginByDllName( string dllName, bool silent = false )
    {
        var pluginDir = FindPluginDirectoryByDllName(dllName);
        if (string.IsNullOrWhiteSpace(pluginDir))
        {
            if (!silent)
            {
                logger.LogWarning("Failed to load plugin by name: {DllName}", dllName);
            }
            return false;
        }

        var oldContext = plugins
            .Where(p => p.Status != PluginStatus.Loading && p.Status != PluginStatus.Loaded)
            .FirstOrDefault(p => p.PluginDirectory?.Trim().Equals(pluginDir.Trim()) ?? false);

        PluginContext? newContext = null;
        try
        {
            if (oldContext != null && plugins.Remove(oldContext))
            {
                newContext = LoadPlugin(pluginDir, true, silent);
                if (newContext?.Status == PluginStatus.Loaded)
                {
                    if (!silent)
                    {
                        logger.LogInformation("Loaded plugin: {Id}", newContext.Metadata!.Id);
                    }
                    return true;
                }
            }
            throw new ArgumentException(string.Empty, string.Empty);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return false;
            }
            if (!silent)
            {
                logger.LogWarning("Failed to load plugin by name: {Path}", pluginDir);
            }
            if (newContext != null)
            {
                newContext.Status = PluginStatus.Indeterminate;
            }
            return false;
        }
        finally
        {
            RebuildSharedServices();
        }
    }

    public bool ReloadPluginById( string id, bool silent = false )
    {
        _ = UnloadPluginById(id, silent);
        return LoadPluginById(id, silent);
    }

    public bool ReloadPluginByDllName( string dllName, bool silent = false )
    {
        _ = UnloadPluginByDllName(dllName, silent);
        return LoadPluginByDllName(dllName, silent);
    }

    private void LoadExports()
    {
        void PopulateSharedManually( string startDirectory )
        {
            EnumeratePluginDirectories(startDirectory, pluginDir =>
            {
                var exportsPath = Path.Combine(pluginDir, "resources", "exports");
                if (!Directory.Exists(exportsPath))
                {
                    return;
                }

                Directory.GetFiles(exportsPath, "*.dll")
                    .ToList()
                    .ForEach(exportFile =>
                    {
                        try
                        {
                            var assembly = Assembly.LoadFrom(exportFile);
                            assembly.GetTypes().ToList().ForEach(sharedTypes.Add);
                        }
                        catch (Exception innerEx)
                        {
                            if (!GlobalExceptionHandler.Handle(innerEx))
                            {
                                return;
                            }
                            logger.LogWarning(innerEx, "Failed to load export assembly: {Path}", exportFile);
                        }
                    });
            });
        }

        try
        {
            var resolver = new DependencyResolver(logger);
            resolver.AnalyzeDependencies(rootDirService.GetPluginsRoot());
            logger.LogInformation("{Graph}", resolver.GetDependencyGraphVisualization());
            var loadOrder = resolver.GetLoadOrder();
            logger.LogInformation("Loading {Count} export assemblies in dependency order", loadOrder.Count);

            loadOrder.ForEach(exportFile =>
            {
                try
                {
                    var assembly = Assembly.LoadFrom(exportFile);
                    var exports = assembly.GetTypes();
                    logger.LogDebug("Loaded {Count} types from {Path}", exports.Length, Path.GetFileName(exportFile));
                    exports.ToList().ForEach(sharedTypes.Add);
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex))
                    {
                        return;
                    }
                    logger.LogWarning(ex, "Failed to load export assembly: {Path}", exportFile);
                }
            });

            logger.LogInformation("Loaded {Count} shared types", sharedTypes.Count);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("circular dependency", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogError(ex, "Circular dependency detected in plugin exports, loading manually");
            PopulateSharedManually(rootDirService.GetPluginsRoot());
        }
        catch (Exception ex)
        {
            if (!GlobalExceptionHandler.Handle(ex))
            {
                return;
            }
            logger.LogError(ex, "Failed to load exports");
        }
    }
    private void LoadPlugins()
    {
        EnumeratePluginDirectories(rootDirService.GetPluginsRoot(), pluginDir =>
        {
            var relativePath = Path.GetRelativePath(rootDirService.GetRoot(), pluginDir);
            var displayPath = Path.Join("(swRoot)", relativePath);
            var dllName = Path.GetFileName(pluginDir);
            var fullDisplayPath = string.IsNullOrWhiteSpace(displayPath) ? string.Empty : $"{Path.Join(displayPath, dllName)}.dll";

            Console.WriteLine(string.Empty);
            logger.LogInformation("Loading plugin: {Path}", fullDisplayPath);

            try
            {
                var context = LoadPlugin(pluginDir, true);
                if (context?.Status == PluginStatus.Loaded)
                {
                    logger.LogInformation(
                        string.Join("\n", [
                            "Loaded Plugin",
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
                    logger.LogWarning("Failed to load plugin: {Path}", fullDisplayPath);
                }
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e))
                {
                    return;
                }
                logger.LogWarning(e, "Failed to load plugin: {Path}", fullDisplayPath);
            }

            Console.WriteLine(string.Empty);
        });

        RebuildSharedServices();

        plugins
            .Where(p => p.Status == PluginStatus.Loaded)
            .ToList()
            .ForEach(p => p.Plugin?.OnAllPluginsLoaded());
    }

    private PluginContext? LoadPlugin( string dir, bool hotReload, bool silent = false )
    {
        PluginContext? FailWithError( PluginContext context, string message )
        {
            if (!silent)
            {
                logger.LogWarning("{Message}", message);
            }
            context.Status = PluginStatus.Error;
            return null;
        }

        var context = new PluginContext { PluginDirectory = dir, Status = PluginStatus.Loading };
        plugins.Add(context);

        var entrypointDll = Path.Combine(dir, Path.GetFileName(dir) + ".dll");
        if (!File.Exists(entrypointDll))
        {
            return FailWithError(context, $"Failed to find plugin entrypoint DLL: {Path.Combine(dir, Path.GetFileName(dir))}.dll");
        }

        var currentContext = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());
        var loader = PluginLoader.CreateFromAssemblyFile(
            entrypointDll,
            [typeof(BasePlugin), .. sharedTypes],
            config =>
            {
                config.IsUnloadable = config.LoadInMemory = true;
                if (currentContext != null)
                {
                    (config.DefaultContext, config.PreferSharedTypes) = (currentContext, true);
                }
            }
        );

        var pluginType = loader.LoadDefaultAssembly()
            .GetTypes()
            .FirstOrDefault(t => t.IsSubclassOf(typeof(BasePlugin)));
        if (pluginType == null)
        {
            return FailWithError(context, $"Failed to find plugin type: {Path.Combine(dir, Path.GetFileName(dir))}.dll");
        }

        var metadata = pluginType.GetCustomAttribute<PluginMetadata>();
        if (metadata == null)
        {
            return FailWithError(context, $"Failed to find plugin metadata: {Path.Combine(dir, Path.GetFileName(dir))}.dll");
        }

        context.Metadata = metadata;
        dataDirectoryService.EnsurePluginDataDirectory(metadata.Id);

        var pluginDir = Path.GetDirectoryName(entrypointDll)!;
        var dataDir = dataDirectoryService.GetPluginDataDirectory(metadata.Id);
        var core = new SwiftlyCore(metadata.Id, pluginDir, metadata, pluginType, rootProvider, dataDir);

        core.InitializeType(pluginType);
        var plugin = (BasePlugin)Activator.CreateInstance(pluginType, [core])!;
        core.InitializeObject(plugin);

        try
        {
            plugin.Load(hotReload);
            context.Status = PluginStatus.Loaded;
            context.Core = core;
            context.Plugin = plugin;
            context.Loader = loader;
            return context;
        }
        catch (Exception e)
        {
            _ = GlobalExceptionHandler.Handle(e);

            try
            {
                plugin.Unload();
                loader?.Dispose();
                core?.Dispose();
            }
            catch (Exception ex)
            {
                if (GlobalExceptionHandler.Handle(ex))
                {
                    AnsiConsole.WriteException(ex);
                }
            }

            logger.LogError(e, "Exception occurred while loading plugin: {PluginPath}", Path.Combine(dir, Path.GetFileName(dir)) + ".dll");

            return FailWithError(context, $"Failed to load plugin: {Path.Combine(dir, Path.GetFileName(dir))}.dll");
        }
    }

    private void RebuildSharedServices()
    {
        interfaceManager.Dispose();

        var loadedPlugins = plugins
            .Where(p => p.Status == PluginStatus.Loaded)
            .ToList();

        loadedPlugins.ForEach(p => p.Plugin?.ConfigureSharedInterface(interfaceManager));
        interfaceManager.Build();

        loadedPlugins.ForEach(p => p.Plugin?.UseSharedInterface(interfaceManager));
        loadedPlugins.ForEach(p => p.Plugin?.OnSharedInterfaceInjected(interfaceManager));
    }

    private static void EnumeratePluginDirectories( string directory, Action<string> action )
    {
        var pluginDirs = Directory.GetDirectories(directory);

        foreach (var pluginDir in pluginDirs)
        {
            var dirName = Path.GetFileName(pluginDir);
            if (dirName.Trim().StartsWith('[') && dirName.EndsWith(']'))
            {
                EnumeratePluginDirectories(pluginDir, action);
                continue;
            }

            if (dirName.Trim().Equals("disable", StringComparison.OrdinalIgnoreCase) || dirName.Trim().Equals("disabled", StringComparison.OrdinalIgnoreCase) || dirName.Trim().Equals("_", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (dirName.Trim().Length >= 2 && dirName.StartsWith('_'))
            {
                continue;
            }

            action(pluginDir);
        }
    }
}