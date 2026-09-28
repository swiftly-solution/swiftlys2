using System.Runtime;
using System.Reflection;
using System.Runtime.InteropServices;
using Spectre.Console;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Core.Plugins;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Plugins;

namespace SwiftlyS2.Core.Services;

internal class CoreCommandService
{
    private readonly ILogger<CoreCommandService> logger;
    private readonly ISwiftlyCore core;
    private readonly PluginManager pluginManager;
    private readonly RootDirService rootDirService;
    private readonly ProfileService profileService;

    public CoreCommandService( ILogger<CoreCommandService> logger, ISwiftlyCore core, PluginManager pluginManager, RootDirService rootDirService, ProfileService profileService )
    {
        this.logger = logger;
        this.core = core;
        this.pluginManager = pluginManager;
        this.rootDirService = rootDirService;
        this.profileService = profileService;
        _ = core.Command.RegisterCommand("sw", OnCommand, true, helpText: "SwiftlyS2 Core Command");
        _ = core.Command.RegisterCommand("buildinfo", ( ctx ) => ctx.Reply($"SwiftlyS2 v{NativeEngineHelpers.GetNativeVersion()}"), true, helpText: "SwiftlyS2 Build Information");
    }

    private void OnCommand( ICommandContext context )
    {
        void ShowPlayerList()
        {
            var output = string.Join("\n", [
                $"Connected players: {core.PlayerManager.PlayerCount}/{core.Engine.GlobalVars.MaxClients}",
                ..core.PlayerManager.GetAllValidPlayers().Select(player => $"{player.PlayerID}. {player.Controller?.PlayerName}{(player.IsFakeClient ? " (BOT)" : "")} (steamid={player.SteamID})")
            ]);
            logger.LogInformation("{Output}", output);
        }

        void ShowServerStatus()
        {
            var uptime = DateTime.Now - System.Diagnostics.Process.GetCurrentProcess().StartTime;
            ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableCompletionPortThreads);
            ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionPortThreads);
            var busyWorkerThreads = maxWorkerThreads - availableWorkerThreads;
            var processThreadCount = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;

            var output = string.Join("\n", [
                $"Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s",
                $"Managed Heap Memory: {GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB",
                $"Process Threads: {processThreadCount}",
                $"ThreadPool Worker Threads: {busyWorkerThreads}/{maxWorkerThreads} (Busy/Max)",
                $"ThreadPool Completion Port Threads: {maxCompletionPortThreads - availableCompletionPortThreads}/{maxCompletionPortThreads} (Busy/Max)",
                $"Loaded Plugins: {pluginManager.GetPlugins().Count}",
                $"Players: {core.PlayerManager.PlayerCount}/{core.Engine.GlobalVars.MaxClients}",
                $"Map: {core.Engine.GlobalVars.MapName.Value}",
            ]);
            logger.LogInformation("{Output}", output);
        }

        void ShowVersionInfo()
        {
            var output = string.Join("\n", [
                $"SwiftlyS2 Version: {NativeEngineHelpers.GetNativeVersion()}",
                $"SwiftlyS2 Managed Version: {Assembly.GetExecutingAssembly().GetName().Version}",
                $"SwiftlyS2 Runtime Version: {Environment.Version}",
                $"SwiftlyS2 C++ Version: C++23",
                $"SwiftlyS2 .NET Version: {RuntimeInformation.FrameworkDescription}",
                $"GitHub URL: https://github.com/swiftly-solution/swiftlys2"
            ]);
            logger.LogInformation("{Output}", output);
        }

        void ShowGarbageCollectionInfo()
        {
            var output = string.Join("\n", [
                $"Garbage Collection Information:",
                $"  - Total Memory: {GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB",
                $"  - Is Server GC: {GCSettings.IsServerGC}",
                $"  - Max Generation: {GC.MaxGeneration}",
                ..Enumerable.Range(0, GC.MaxGeneration + 1).Select(i => $"    - Generation {i} Collection Count: {GC.CollectionCount(i)}"),
                $"  - Latency Mode: {GCSettings.LatencyMode}"
            ]);
            logger.LogInformation("{Output}", output);
        }

        void ShowCredits()
        {
            var output = string.Join("\n", [
                "SwiftlyS2 was created and developed by Swiftly Solution SRL and the contributors.",
                "SwiftlyS2 is licensed under the GNU General Public License v3.0 or later.",
                "Website: https://swiftlys2.net/",
                "GitHub: https://github.com/swiftly-solution/swiftlys2"
            ]);
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
                case "translations" when RequireConsoleAccess():
                    TranslationsCommand(context);
                    break;
                case "cmds" when RequireConsoleAccess():
                    CommandsCommand(context);
                    break;
                default:
                    ShowHelp(context);
                    break;
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            logger.LogError(e, "Failed to execute command");
        }
    }

    private static void ShowHelp( ICommandContext context )
    {
        var table = new Table()
            .AddColumn("Command").AddColumn("Description")
            .AddRow("credits", "List Swiftly credits")
            .AddRow("help", "Show the help for Swiftly Commands")
            .AddRow("list", "Show the list of online players")
            .AddRow("status", "Show the status of the server");
        if (!context.IsSentByPlayer)
        {
            _ = table
                .AddRow(Markup.Escape("cmds [page]"), "List all plugin commands (paginated, 20 per page)")
                .AddRow("confilter", "Console Filter Menu")
                .AddRow("plugins", "Plugin Management Menu")
                .AddRow("gc", "Show garbage collection information on managed")
                .AddRow("profiler", "Profiler Menu")
                .AddRow("translations", "Translations Menu");
        }
        _ = table.AddRow("version", "Display Swiftly version");
        AnsiConsole.Write(table);
    }

    private void TranslationsCommand( ICommandContext context )
    {
        void ShowTranslationsHelp()
        {
            var table = new Table()
                .AddColumn("Command")
                .AddColumn("Description")
                .AddRow("reload", "Reload all translations");
            AnsiConsole.Write(table);
        }

        void ReloadTranslations()
        {
            pluginManager.RegenerateTranslations();

            logger.LogInformation("Succesfully reloaded the translations");
        }

        var args = context.Args;
        if (args.Length == 1)
        {
            ShowTranslationsHelp();
            return;
        }

        switch (args[1].Trim().ToLower())
        {
            case "reload":
                ReloadTranslations();
                break;
            default:
                logger.LogWarning("Unknown command");
                break;
        }
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
            var output = string.Join("\n", [
                $"Console filtering is currently {status}.",
                "Below are some statistics for the filtering process:",
                core.ConsoleOutput.GetCounterText()
            ]);
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
            var table = new Table().AddColumn("Command").AddColumn("Description")
                .AddRow("enable <1|2>", "Enable the profiler (1 = light/EventPipe, 2 = heavy/Harmony)")
                .AddRow("disable", "Disable the profiler")
                .AddRow("status", "Show the status of the profiler")
                .AddRow("save", "Save the profiler data to a file");
            AnsiConsole.Write(table);
            return;
        }

        switch (args[1].Trim().ToLower())
        {
            case "enable":
                var levelArg = args.Length > 2 ? args[2].Trim() : "1";
                if (!int.TryParse(levelArg, out var levelValue) || levelValue is not (1 or 2))
                {
                    logger.LogWarning("Usage: profiler enable <1|2> (1 = light, 2 = heavy)");
                    break;
                }
                var level = (ProfilerLevel)levelValue;
                if (level == ProfilerLevel.Heavy)
                    logger.LogWarning("Heavy mode patches the core SwiftlyS2 assembly, SwiftlyS2.Profiler, and every loaded plugin with Harmony - this will add per-call overhead while active.");
                profileService.Enable(level);
                logger.LogInformation("The profiler has been enabled ({Level}).", level);
                break;
            case "disable":
                profileService.Disable();
                logger.LogInformation("The profiler has been disabled.");
                break;
            case "status":
                logger.LogInformation("Profiler is currently {Status}.", profileService.CurrentLevel);
                break;
            case "save":
                _ = profileService.SaveAsync(rootDirService.GetRoot(), logger);
                break;
            default:
                logger.LogWarning("Unknown command");
                break;
        }
    }

    private void PluginCommand( ICommandContext context )
    {
        void ShowPluginList( int page )
        {
            const int pageSize = 20;

            if (page < 1) page = 1;

            var allPlugins = pluginManager.GetPlugins().ToList();
            var totalPages = (int)Math.Ceiling(allPlugins.Count / (double)pageSize);
            if (totalPages == 0) totalPages = 1;

            if (page > totalPages)
            {
                logger.LogWarning("Page {Page} out of range. Total pages: {Total}", page, totalPages);
                return;
            }

            var pagePlugins = allPlugins.Skip((page - 1) * pageSize).Take(pageSize);

            var table = new Table()
                .AddColumn("Status")
                .AddColumn("PluginId (ver.)")
                .AddColumn("Author")
                .AddColumn("Location");

            foreach (var plugin in pagePlugins)
            {
                var pluginId = Markup.Escape(plugin.Metadata?.Id ?? "<Unknown>");
                var version = Markup.Escape(plugin.Metadata?.Version is { } v ? $" {v}" : string.Empty);
                var statusText = GetColoredStatus(plugin.Status);

                _ = table.AddRow(
                    statusText,
                    $"{pluginId}{version}",
                    Markup.Escape(plugin.Metadata?.Author ?? "Anonymous"),
                    Markup.Escape(plugin.PluginDirectory is { } dir ? Path.Join("(swRoot)", Path.GetRelativePath(rootDirService.GetRoot(), dir)) : string.Empty));
            }

            logger.LogInformation("Plugins (page {Page}/{Total}, {Count} total):", page, totalPages, allPlugins.Count);
            AnsiConsole.Write(table);

            if (page < totalPages)
            {
                logger.LogInformation("Use 'sw plugins list {Next}' to see the next page.", page + 1);
            }

            var loadErrors = pluginManager.GetPluginLoadErrors();
            if (loadErrors.Count > 0)
            {
                Console.WriteLine("\n");
                var errorString = "Plugin Load Errors:";
                foreach (var error in loadErrors)
                {
                    errorString += $"\n  {error.Key}: {error.Value}";
                }
                logger.LogWarning(errorString);
            }
        }

        void ShowPluginHelp()
        {
            var table = new Table()
                .AddColumn("Command")
                .AddColumn("Description")
                .AddRow(Markup.Escape("list [page]"), "List all plugins (paginated, 20 per page)")
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

        string GetColoredStatus( PluginStatus? status ) => status switch {
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
            _ => "Unknown"
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
                var listPage = args.Length >= 3 && int.TryParse(args[2], out var parsedListPage) ? parsedListPage : 1;
                ShowPluginList(listPage);
                break;
            case "load":
                if (ValidatePluginId(args, "load", "<dllName>"))
                {
                    Console.WriteLine("\n");
                    if (pluginManager.GetPluginStatusByDllName(args[2]) == PluginStatus.Loaded)
                    {
                        logger.LogWarning("Plugin is already loaded: {Format}", args[2]);
                        Console.WriteLine("\n");
                        break;
                    }

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
                if (ValidatePluginId(args, "unload", "<dllName>"))
                {
                    Console.WriteLine("\n");
                    if (pluginManager.UnloadPluginByDllName(args[2], true))
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
                if (ValidatePluginId(args, "reload", "<dllName>"))
                {
                    Console.WriteLine("\n");
                    if (pluginManager.ReloadPluginByDllName(args[2], true))
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

    private void CommandsCommand( ICommandContext context )
    {
        const int pageSize = 20;

        var args = context.Args;
        var page = 1;

        if (args.Length >= 2 && int.TryParse(args[1], out var parsedPage))
        {
            page = parsedPage;
        }

        if (page < 1)
        {
            page = 1;
        }

        var commandsByPlugin = core.Command.GetAllCommandsByPlugin();

        if (commandsByPlugin.Count == 0)
        {
            logger.LogInformation("No commands registered.");
            return;
        }

        var allRows = commandsByPlugin
            .OrderBy(x => x.Key)
            .SelectMany(pluginEntry => pluginEntry.Value
                .OrderBy(x => x.CommandName)
                .Select(( cmd, idx ) => (
                    Plugin: idx == 0 ? pluginEntry.Key : string.Empty,
                    cmd.CommandName,
                    cmd.HelpText,
                    cmd.Permission)))
            .ToList();

        var totalPages = (int)Math.Ceiling(allRows.Count / (double)pageSize);
        if (totalPages == 0) totalPages = 1;

        if (page > totalPages)
        {
            logger.LogWarning("Page {Page} out of range. Total pages: {Total}", page, totalPages);
            return;
        }

        var pageRows = allRows.Skip((page - 1) * pageSize).Take(pageSize);

        var table = new Table()
            .AddColumn("Plugin")
            .AddColumn("Command Name")
            .AddColumn("Help Text")
            .AddColumn("Permission");

        foreach (var (plugin, commandName, helpText, permission) in pageRows)
        {
            _ = table.AddRow(
                plugin,
                commandName,
                Markup.Escape(helpText),
                string.IsNullOrWhiteSpace(permission) ? "(none)" : permission);
        }

        logger.LogInformation("Commands (page {Page}/{Total}, {Count} total):", page, totalPages, allRows.Count);
        AnsiConsole.Write(table);

        if (page < totalPages)
        {
            logger.LogInformation("Use 'sw cmds {Next}' to see the next page.", page + 1);
        }
    }
}