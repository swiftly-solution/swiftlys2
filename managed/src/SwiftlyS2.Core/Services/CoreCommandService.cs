using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Plugins;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Commands;

namespace SwiftlyS2.Core.Services;

internal class CoreCommandService
{
    private readonly ILogger<CoreCommandService> logger;
    private readonly ISwiftlyCore core;
    private readonly PluginManager pluginManager;
    private readonly RootDirService rootDirService;
    private readonly ProfileService profileService;

    public CoreCommandService(
        ILogger<CoreCommandService> logger,
        ISwiftlyCore core,
        PluginManager pluginManager,
        RootDirService rootDirService,
        ProfileService profileService
    )
    {
        this.logger = logger;
        this.core = core;
        this.pluginManager = pluginManager;
        this.rootDirService = rootDirService;
        this.profileService = profileService;
        _ = core.Command.RegisterCommand("sw", OnCommand, true);
    }

    private void OnCommand( ICommandContext context )
    {
        void ShowPlayerList()
        {
            var output = string.Join(
                "\n",
                [
                    $"Connected players: {core.PlayerManager.PlayerCount}/{core.Engine.GlobalVars.MaxClients}",
                    .. core
                        .PlayerManager.GetAllPlayers()
                        .Select(player =>
                            $"{player.PlayerID}. {player.Controller?.PlayerName}{(player.IsFakeClient ? " (BOT)" : "")} (steamid={player.SteamID})"
                        ),
                ]
            );
            logger.LogInformation("{Output}", output);
        }

        void ShowServerStatus()
        {
            var uptime = DateTime.Now - System.Diagnostics.Process.GetCurrentProcess().StartTime;
            var output = string.Join(
                "\n",
                [
                    $"Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s",
                    $"Managed Heap Memory: {GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB",
                    $"Loaded Plugins: {pluginManager.GetPlugins().Count}",
                    $"Players: {core.PlayerManager.PlayerCount}/{core.Engine.GlobalVars.MaxClients}",
                    $"Map: {core.Engine.GlobalVars.MapName.Value}",
                ]
            );
            logger.LogInformation("{Output}", output);
        }

        void ShowVersionInfo()
        {
            var output = string.Join(
                "\n",
                [
                    $"SwiftlyS2 Version: {NativeEngineHelpers.GetNativeVersion()}",
                    $"SwiftlyS2 Managed Version: {Assembly.GetExecutingAssembly().GetName().Version}",
                    $"SwiftlyS2 Runtime Version: {Environment.Version}",
                    $"SwiftlyS2 C++ Version: C++23",
                    $"SwiftlyS2 .NET Version: {RuntimeInformation.FrameworkDescription}",
                    $"GitHub URL: https://github.com/swiftly-solution/swiftlys2",
                ]
            );
            logger.LogInformation("{Output}", output);
        }

        void ShowGarbageCollectionInfo()
        {
            var output = string.Join(
                "\n",
                [
                    $"Garbage Collection Information:",
                    $"  - Total Memory: {GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB",
                    $"  - Is Server GC: {GCSettings.IsServerGC}",
                    $"  - Max Generation: {GC.MaxGeneration}",
                    .. Enumerable
                        .Range(0, GC.MaxGeneration + 1)
                        .Select(i =>
                            $"    - Generation {i} Collection Count: {GC.CollectionCount(i)}"
                        ),
                    $"  - Latency Mode: {GCSettings.LatencyMode}",
                ]
            );
            logger.LogInformation("{Output}", output);
        }

        void ShowCredits()
        {
            var output = string.Join(
                "\n",
                [
                    "SwiftlyS2 was created and developed by Swiftly Solution SRL and the contributors.",
                    "SwiftlyS2 is licensed under the GNU General Public License v3.0 or later.",
                    "Website: https://swiftlys2.net/",
                    "GitHub: https://github.com/swiftly-solution/swiftlys2",
                ]
            );
            logger.LogInformation("{Output}", output);
        }

        bool RequireConsoleAccess()
        {
            if (context.IsSentByPlayer)
            {
                context.Reply("This command can only be executed from the server console.");
                return false;
            }
            return true;
        }

        try
        {
            if (context.IsSentByPlayer)
            {
                return;
            }

            var args = context.Args;
            if (args.Length == 0)
            {
                ShowHelp(context);
                return;
            }

            switch (args[0].Trim().ToLower())
            {
                case "help":
                    ShowHelp(context);
                    break;
                case "credits":
                    ShowCredits();
                    break;
                case "list":
                    ShowPlayerList();
                    break;
                case "status":
                    ShowServerStatus();
                    break;
                case "version":
                    ShowVersionInfo();
                    break;
                case "gc" when RequireConsoleAccess():
                    ShowGarbageCollectionInfo();
                    break;
                case "plugins" when RequireConsoleAccess():
                    PluginCommand(context);
                    break;
                case "profiler" when RequireConsoleAccess():
                    ProfilerCommand(context);
                    break;
                case "confilter" when RequireConsoleAccess():
                    ConfilterCommand(context);
                    break;
                default:
                    ShowHelp(context);
                    break;
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            logger.LogError(e, "Failed to execute command");
        }
    }

    private static void ShowHelp( ICommandContext context )
    {
        var table = new Table()
            .AddColumn("Command")
            .AddColumn("Description")
            .AddRow("credits", "List Swiftly credits")
            .AddRow("help", "Show the help for Swiftly Commands")
            .AddRow("list", "Show the list of online players")
            .AddRow("status", "Show the status of the server");
        if (!context.IsSentByPlayer)
        {
            _ = table
                .AddRow("confilter", "Console Filter Menu")
                .AddRow("plugins", "Plugin Management Menu")
                .AddRow("gc", "Show garbage collection information on managed")
                .AddRow("profiler", "Profiler Menu");
        }
        _ = table.AddRow("version", "Display Swiftly version");
        AnsiConsole.Write(table);
    }

    private void ConfilterCommand( ICommandContext context )
    {
        void ShowConfilterHelp()
        {
            var table = new Table()
                .AddColumn("Command")
                .AddColumn("Description")
                .AddRow("enable", "Enable console filtering")
                .AddRow("disable", "Disable console filtering")
                .AddRow("status", "Show the status of the console filter")
                .AddRow("reload", "Reload console filter configuration");
            AnsiConsole.Write(table);
        }

        void EnableFilter()
        {
            if (!core.ConsoleOutput.IsFilterEnabled())
            {
                core.ConsoleOutput.ToggleFilter();
            }
            logger.LogInformation("Console filtering has been enabled.");
        }

        void DisableFilter()
        {
            if (core.ConsoleOutput.IsFilterEnabled())
            {
                core.ConsoleOutput.ToggleFilter();
            }
            logger.LogInformation("Console filtering has been disabled.");
        }

        void ShowFilterStatus()
        {
            var status = core.ConsoleOutput.IsFilterEnabled() ? "enabled" : "disabled";
            var output = string.Join(
                "\n",
                [
                    $"Console filtering is currently {status}.",
                    "Below are some statistics for the filtering process:",
                    core.ConsoleOutput.GetCounterText(),
                ]
            );
            logger.LogInformation("{Output}", output);
        }

        void ReloadFilter()
        {
            core.ConsoleOutput.ReloadFilterConfiguration();
            logger.LogInformation("Console filter configuration reloaded.");
        }

        var args = context.Args;
        if (args.Length == 1)
        {
            ShowConfilterHelp();
            return;
        }

        switch (args[1].Trim().ToLower())
        {
            case "enable":
                EnableFilter();
                break;
            case "disable":
                DisableFilter();
                break;
            case "status":
                ShowFilterStatus();
                break;
            case "reload":
                ReloadFilter();
                break;
            default:
                logger.LogWarning("Unknown command");
                break;
        }
    }

    private void ProfilerCommand( ICommandContext context )
    {
        var args = context.Args;
        if (args.Length == 1)
        {
            var table = new Table()
                .AddColumn("Command")
                .AddColumn("Description")
                .AddRow("enable", "Enable the profiler")
                .AddRow("disable", "Disable the profiler")
                .AddRow("status", "Show the status of the profiler")
                .AddRow("save", "Save the profiler data to a file");
            AnsiConsole.Write(table);
            return;
        }

        switch (args[1].Trim().ToLower())
        {
            case "enable":
                profileService.Enable();
                logger.LogInformation("The profiler has been enabled.");
                break;
            case "disable":
                profileService.Disable();
                logger.LogInformation("The profiler has been disabled.");
                break;
            case "status":
                logger.LogInformation(
                    "Profiler is currently {Status}.",
                    (profileService.IsEnabled() ? "enabled" : "disabled")
                );
                break;
            case "save":
                var pluginId = args.Length >= 3 ? args[2] : "core";
                var profilerDir = Path.Combine(rootDirService.GetRoot(), "profilers");

                if (!Directory.Exists(profilerDir))
                {
                    _ = Directory.CreateDirectory(profilerDir);
                }

                var fileName = $"{DateTime.Now:yyyyMMdd}.{Guid.NewGuid()}.{pluginId}.json";
                var filePath = Path.Combine(profilerDir, fileName);

                File.WriteAllText(
                    filePath,
                    profileService.GenerateJSONPerformance(
                        args.Length >= 3 ? args[2] : string.Empty
                    )
                );
                logger.LogInformation("Profile saved to {FilePath}.", filePath);
                break;
            default:
                logger.LogWarning("Unknown command");
                break;
        }
    }

    private void PluginCommand( ICommandContext context )
    {
        void ShowPluginList()
        {
            var table = new Table()
                .AddColumn("Status")
                .AddColumn("PluginId (ver.)")
                .AddColumn("Author")
                .AddColumn("Website")
                .AddColumn("Location");

            foreach (var plugin in pluginManager.GetPlugins())
            {
                var pluginId = plugin.Metadata?.Id ?? "<Unknown>";
                var version = plugin.Metadata?.Version is { } v ? $" {v}" : string.Empty;
                var statusText = GetColoredStatus(plugin.Status);

                _ = table.AddRow(
                    statusText,
                    $"{pluginId}{version}",
                    plugin.Metadata?.Author ?? "Anonymous",
                    plugin.Metadata?.Website ?? string.Empty,
                    plugin.PluginDirectory is { } dir
                        ? Path.Join("(swRoot)", Path.GetRelativePath(rootDirService.GetRoot(), dir))
                        : string.Empty
                );
            }

            AnsiConsole.Write(table);
        }

        void ShowPluginHelp()
        {
            var table = new Table()
                .AddColumn("Command")
                .AddColumn("Description")
                .AddRow("list", "List all plugins")
                .AddRow("load", "Load a plugin")
                .AddRow("unload", "Unload a plugin")
                .AddRow("reload", "Reload a plugin");
            AnsiConsole.Write(table);
        }

        bool ValidatePluginId( string[] args, string command, string usage )
        {
            if (args.Length >= 3)
            {
                return true;
            }
            logger.LogWarning("Usage: sw plugins {Command} {Usage}", command, usage);
            return false;
        }

        string GetColoredStatus( PluginStatus? status ) =>
            status switch {
                // PluginStatus.Loaded => "[green]Loaded[/]",
                // PluginStatus.Error => "[red]Error[/]",
                // PluginStatus.Loading => "[yellow]Loading[/]",
                // PluginStatus.Unloaded => "[grey]Unloaded[/]",
                // _ => "[grey]Unknown[/]"
                PluginStatus.Loaded => "Loaded",
                PluginStatus.Error => "Error",
                PluginStatus.Loading => "Loading",
                PluginStatus.Unloaded => "Unloaded",
                PluginStatus.Indeterminate => "Indeterminate",
                _ => "Unknown",
            };

        var args = context.Args;
        if (args.Length == 1)
        {
            ShowPluginHelp();
            return;
        }

        switch (args[1].Trim().ToLower())
        {
            case "list":
                ShowPluginList();
                break;
            case "load":
                if (ValidatePluginId(args, "load", "<dllName>"))
                {
                    Console.WriteLine("\n");
                    if (pluginManager.LoadPluginByDllName(args[2], true))
                    {
                        logger.LogInformation("Loaded plugin: {Format}", args[2]);
                    }
                    else
                    {
                        logger.LogWarning("Failed to load plugin: {Format}", args[2]);
                    }
                    Console.WriteLine("\n");
                }
                break;
            case "unload":
                if (ValidatePluginId(args, "unload", "<pluginId|dllName>"))
                {
                    Console.WriteLine("\n");
                    if (
                        pluginManager.UnloadPluginById(args[2], true)
                        || pluginManager.UnloadPluginByDllName(args[2], true)
                    )
                    {
                        logger.LogInformation("Unloaded plugin: {Format}", args[2]);
                    }
                    else
                    {
                        logger.LogWarning("Failed to unload plugin: {Format}", args[2]);
                    }
                    Console.WriteLine("\n");
                }
                break;
            case "reload":
                if (ValidatePluginId(args, "reload", "<pluginId|dllName>"))
                {
                    Console.WriteLine("\n");
                    if (
                        pluginManager.ReloadPluginById(args[2], true)
                        || pluginManager.ReloadPluginByDllName(args[2], true)
                    )
                    {
                        logger.LogInformation("Reloaded plugin: {Format}", args[2]);
                    }
                    else
                    {
                        logger.LogWarning("Failed to reload plugin: {Format}", args[2]);
                    }
                    Console.WriteLine("\n");
                }
                break;
            default:
                logger.LogWarning("Unknown command");
                break;
        }
    }
}
