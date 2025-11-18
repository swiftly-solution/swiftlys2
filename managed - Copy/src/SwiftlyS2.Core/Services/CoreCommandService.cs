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
  private ILogger<CoreCommandService> _Logger { get; init; }

  private ISwiftlyCore _Core { get; init; }

  private ICommandService _CommandService { get; init; }
  private PluginManager _PluginManager { get; init; }
  private ProfileService _ProfileService { get; init; }

  public CoreCommandService( ILogger<CoreCommandService> logger, ISwiftlyCore core, PluginManager pluginManager, ProfileService profileService )
  {
    _Logger = logger;
    _Core = core;
    _CommandService = core.Command;
    _PluginManager = pluginManager;
    _ProfileService = profileService;
    _CommandService.RegisterCommand("sw", OnCommand, true);
  }

  private void OnCommand( ICommandContext context )
  {
    try
    {
      if (context.IsSentByPlayer) return;

      var args = context.Args;
      if (args.Length == 0)
      {
        ShowHelp(context);
        return;
      }

      switch (args[0])
      {
        case "help":
          ShowHelp(context);
          break;
        case "credits":
          _Logger.LogInformation(@"SwiftlyS2 was created and developed by Swiftly Solution SRL and the contributors.
SwiftlyS2 is licensed under the GNU General Public License v3.0 or later.
Website: https://swiftlys2.net/
GitHub: https://github.com/swiftly-solution/swiftlys2");
          break;
        case "list":
          var players = _Core.PlayerManager.GetAllPlayers();
          var outString = $"Connected players: {_Core.PlayerManager.PlayerCount}/{_Core.Engine.MaxPlayers}";
          foreach (var player in players)
          {
            outString += $"\n{player.PlayerID}. {player.Controller?.PlayerName}{(player.IsFakeClient ? " (BOT)" : "")} (steamid={player.SteamID})";
          }
          _Logger.LogInformation(outString);
          break;
        case "status":
          var uptime = DateTime.Now - System.Diagnostics.Process.GetCurrentProcess().StartTime;
          var outStrings = $"Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
          outStrings += $"\nManaged Heap Memory: {GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB";
          outStrings += $"\nLoaded Plugins: {_PluginManager.GetPlugins().Count()}";
          outStrings += $"\nPlayers: {_Core.PlayerManager.PlayerCount}/{_Core.Engine.GlobalVars.MaxClients}";
          outStrings += $"\nMap: {_Core.Engine.GlobalVars.MapName.Value}";
          _Logger.LogInformation(outStrings);
          break;
        case "version":
          var outVersion = $"SwiftlyS2 Version: {NativeEngineHelpers.GetNativeVersion()}";
          outVersion += $"\nSwiftlyS2 Managed Version: {Assembly.GetExecutingAssembly().GetName().Version}";
          outVersion += $"\nSwiftlyS2 Runtime Version: {Environment.Version}";
          outVersion += $"\nSwiftlyS2 C++ Version: C++23";
          outVersion += $"\nSwiftlyS2 .NET Version: {RuntimeInformation.FrameworkDescription}";
          outVersion += $"\nGitHub URL: https://github.com/swiftly-solution/swiftlys2";
          _Logger.LogInformation(outVersion);
          break;
        case "gc":
          if (context.IsSentByPlayer)
          {
            context.Reply("This command can only be executed from the server console.");
            return;
          }
          var outGc = "Garbage Collection Information:";
          outGc += $"\n  - Total Memory: {GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB";
          outGc += $"\n  - Is Server GC: {GCSettings.IsServerGC}";
          outGc += $"\n  - Max Generation: {GC.MaxGeneration}";
          for (int i = 0; i <= GC.MaxGeneration; i++)
          {
            outGc += $"\n    - Generation {i} Collection Count: {GC.CollectionCount(i)}";
          }
          outGc += $"\n  - Latency Mode: {GCSettings.LatencyMode}";
          _Logger.LogInformation(outGc);
          break;
        case "plugins":
          if (context.IsSentByPlayer)
          {
            context.Reply("This command can only be executed from the server console.");
            return;
          }
          PluginCommand(context);
          break;
        case "profiler":
          if (context.IsSentByPlayer)
          {
            context.Reply("This command can only be executed from the server console.");
            return;
          }
          ProfilerCommand(context);
          break;
        case "confilter":
          if (context.IsSentByPlayer)
          {
            context.Reply("This command can only be executed from the server console.");
            return;
          }
          ConfilterCommand(context);
          break;
        default:
          ShowHelp(context);
          break;
      }
    }
    catch (Exception e)
    {
      if (!GlobalExceptionHandler.Handle(e)) return;
      _Logger.LogError(e, "Error executing command");
    }
  }

  private static void ShowHelp( ICommandContext context )
  {
    var table = new Table().AddColumn("Command").AddColumn("Description");
    table.AddRow("credits", "List Swiftly credits");
    table.AddRow("help", "Show the help for Swiftly Commands");
    table.AddRow("list", "Show the list of online players");
    table.AddRow("status", "Show the status of the server");
    if (!context.IsSentByPlayer)
    {
      table.AddRow("confilter", "Console Filter Menu");
      table.AddRow("plugins", "Plugin Management Menu");
      table.AddRow("gc", "Show garbage collection information on managed");
      table.AddRow("profiler", "Profiler Menu");
    }
    table.AddRow("version", "Display Swiftly version");
    AnsiConsole.Write(table);
  }

  private void ConfilterCommand( ICommandContext context )
  {
    var args = context.Args;
    if (args.Length == 1)
    {
      var table = new Table().AddColumn("Command").AddColumn("Description");
      table.AddRow("enable", "Enable console filtering");
      table.AddRow("disable", "Disable console filtering");
      table.AddRow("status", "Show the status of the console filter");
      table.AddRow("reload", "Reload console filter configuration");
      AnsiConsole.Write(table);
      return;
    }

    switch (args[1])
    {
      case "enable":
        if (!_Core.ConsoleOutput.IsFilterEnabled()) _Core.ConsoleOutput.ToggleFilter();
        _Logger.LogInformation("Console filtering has been enabled.");
        break;
      case "disable":
        if (_Core.ConsoleOutput.IsFilterEnabled()) _Core.ConsoleOutput.ToggleFilter();
        _Logger.LogInformation("Console filtering has been disabled.");
        break;
      case "status":
        _Logger.LogInformation($"Console filtering is currently {(_Core.ConsoleOutput.IsFilterEnabled() ? "enabled" : "disabled")}.\nBelow are some statistics for the filtering process:\n{_Core.ConsoleOutput.GetCounterText()}");
        break;
      case "reload":
        _Core.ConsoleOutput.ReloadFilterConfiguration();
        _Logger.LogInformation("Console filter configuration reloaded.");
        break;
      default:
        _Logger.LogWarning("Unknown command");
        break;
    }
  }

  private void ProfilerCommand( ICommandContext context )
  {
    var args = context.Args;
    if (args.Length == 1)
    {
      var table = new Table().AddColumn("Command").AddColumn("Description");
      table.AddRow("enable", "Enable the profiler");
      table.AddRow("disable", "Disable the profiler");
      table.AddRow("status", "Show the status of the profiler");
      table.AddRow("save", "Save the profiler data to a file");
      AnsiConsole.Write(table);
      return;
    }

    switch (args[1])
    {
      case "enable":
        _ProfileService.Enable();
        _Logger.LogInformation("The profiler has been enabled.");
        break;
      case "disable":
        _ProfileService.Disable();
        _Logger.LogInformation("The profiler has been disabled.");
        break;
      case "status":
        _Logger.LogInformation($"Profiler is currently {(_ProfileService.IsEnabled() ? "enabled" : "disabled")}.");
        break;
      case "save":
        var pluginId = args.Length >= 3 ? args[2] : "";
        var basePath = Environment.GetEnvironmentVariable("SWIFTLY_MANAGED_ROOT")!;
        if (!File.Exists(Path.Combine(basePath, "profilers")))
        {
          Directory.CreateDirectory(Path.Combine(basePath, "profilers"));
        }

        Guid guid = Guid.NewGuid();
        File.WriteAllText(Path.Combine(basePath, "profilers", $"profiler.{guid}.{(pluginId == "" ? "core" : pluginId)}.json"), _ProfileService.GenerateJSONPerformance(pluginId));
        _Logger.LogInformation($"Profile saved to {Path.Combine(basePath, "profilers", $"profiler.{guid}.{(pluginId == "" ? "core" : pluginId)}.json")}");
        break;
      default:
        _Logger.LogWarning("Unknown command");
        break;
    }
  }

  private void PluginCommand( ICommandContext context )
  {
    var args = context.Args;
    if (args.Length == 1)
    {
      var table = new Table().AddColumn("Command").AddColumn("Description");
      table.AddRow("list", "List all plugins");
      table.AddRow("load", "Load a plugin");
      table.AddRow("unload", "Unload a plugin");
      table.AddRow("reload", "Reload a plugin");
      AnsiConsole.Write(table);
      return;
    }

    switch (args[1])
    {
      case "list":
        var table = new Table().AddColumn("Name").AddColumn("Status").AddColumn("Version").AddColumn("Author").AddColumn("Website");
        foreach (var plugin in _PluginManager.GetPlugins())
        {
          table.AddRow(plugin.Metadata?.Id ?? "<UNKNOWN>", plugin.Status?.ToString() ?? "Unknown", plugin.Metadata?.Version ?? "<UNKNOWN>", plugin.Metadata?.Author ?? "<UNKNOWN>", plugin.Metadata?.Website ?? "<UNKNOWN>");
        }
        AnsiConsole.Write(table);
        break;
      case "load":
        if (args.Length < 3)
        {
          _Logger.LogWarning("Usage: sw plugins load <pluginId>");
          return;
        }
        _PluginManager.LoadPluginById(args[2]);
        break;
      case "unload":
        if (args.Length < 3)
        {
          _Logger.LogWarning("Usage: sw plugins unload <pluginId>");
          return;
        }
        _PluginManager.UnloadPlugin(args[2]);
        break;
      case "reload":
        if (args.Length < 3)
        {
          _Logger.LogWarning("Usage: sw plugins reload <pluginId>");
          return;
        }
        _PluginManager.ReloadPlugin(args[2]);
        break;
      default:
        _Logger.LogWarning("Unknown command");
        break;
    }
  }
}