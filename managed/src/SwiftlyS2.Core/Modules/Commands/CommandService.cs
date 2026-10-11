using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Core.Models;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Permissions;

namespace SwiftlyS2.Core.Commands;

internal class CommandService : ICommandService, IDisposable
{
    private readonly ILoggerFactory loggerFactory;
    private readonly IContextedProfilerService profiler;
    private readonly IPlayerManagerService playerManagerService;
    private readonly IPermissionManager permissionManager;
    private readonly IOptionsMonitor<CommandOverrideConfig> commandOverrideOptions;
    private readonly CoreContext coreContext;

    private readonly List<CommandCallbackBase> commandCallbacks = [];
    private readonly List<ulong> commandAliases = [];
    private readonly List<string> commandAliasNames = [];
    private readonly Lock commandLock = new();

    public CommandService( ILoggerFactory loggerFactory, IContextedProfilerService profiler, IPlayerManagerService playerManagerService, IPermissionManager permissionManager, IOptionsMonitor<CommandOverrideConfig> commandOverrideOptions, CoreContext coreContext )
    {
        this.loggerFactory = loggerFactory;
        this.profiler = profiler;
        this.playerManagerService = playerManagerService;
        this.permissionManager = permissionManager;
        this.commandOverrideOptions = commandOverrideOptions;
        this.coreContext = coreContext;
    }

    public Guid RegisterCommand( string commandName, ICommandService.CommandListener handler, bool registerRaw, string permission )
    {
        return RegisterCommand(commandName, handler, registerRaw, permission, "SwiftlyS2 registered command");
    }

    public Guid RegisterCommand( string commandName, ICommandService.CommandListener handler, bool registerRaw = false, string permission = "", string helpText = "SwiftlyS2 registered command" )
    {
        return AddCallback(new CommandCallback(commandName, registerRaw, handler, permission, helpText, playerManagerService, permissionManager, commandOverrideOptions, loggerFactory, profiler, coreContext.Name));
    }

    public void RegisterCommandAlias( string commandName, string alias, bool registerRaw = false )
    {
        lock (commandLock)
        {
            var commandKey = CommandDispatcher.ResolveCommandKey(commandName);

            var aliasId = NativeCommands.RegisterAlias(alias, commandName, registerRaw);
            if (aliasId == 0)
            {
                return;
            }

            commandAliases.Add(aliasId);

            if (commandKey is null)
            {
                return;
            }

            var normalizedAlias = CommandDispatcher.NormalizeName(alias, registerRaw);
            CommandDispatcher.AddAlias(normalizedAlias, commandKey);
            commandAliasNames.Add(normalizedAlias);
        }
    }

    public void UnregisterCommand( Guid guid ) => RemoveCallbacks(callback => callback.Guid == guid);

    public void UnregisterCommand( string commandName ) =>
        RemoveCallbacks(callback => callback is CommandCallback command
            && (command.CommandName.Equals(commandName, StringComparison.OrdinalIgnoreCase)
                || ("sw_" + command.CommandName).Equals(commandName, StringComparison.OrdinalIgnoreCase)));

    public bool IsCommandRegistered( string commandName )
    {
        return NativeCommands.IsCommandRegistered(commandName);
    }

    public Guid HookClientCommand( ICommandService.ClientCommandHandler handler )
    {
        return AddCallback(new ClientCommandListenerCallback(handler, loggerFactory, profiler, coreContext.Name));
    }

    public void UnhookClientCommand( Guid guid ) =>
        RemoveCallbacks(callback => callback is ClientCommandListenerCallback && callback.Guid == guid);

    public Guid HookClientChat( ICommandService.ClientChatHandler handler )
    {
        return AddCallback(new ClientChatListenerCallback(handler, loggerFactory, profiler, coreContext.Name));
    }

    public void UnhookClientChat( Guid guid ) =>
        RemoveCallbacks(callback => callback is ClientChatListenerCallback && callback.Guid == guid);

    public List<string> GetAllCommands()
    {
        return CommandDispatcher.GetCommands()
            .Select(command => command.CommandName)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public List<CommandInfo> GetCommandsByPlugin( string pluginName )
    {
        return CommandDispatcher.GetCommands()
            .Where(command => command.PluginName == pluginName)
            .Select(ToCommandInfo)
            .ToList();
    }

    public Dictionary<string, List<CommandInfo>> GetAllCommandsByPlugin()
    {
        return CommandDispatcher.GetCommands()
            .GroupBy(command => command.PluginName)
            .ToDictionary(group => group.Key, group => group.Select(ToCommandInfo).ToList());
    }

    public List<CommandInfo> GetAllCommandsInfo()
    {
        return CommandDispatcher.GetCommands()
            .Select(ToCommandInfo)
            .ToList();
    }

    public void Dispose()
    {
        lock (commandLock)
        {
            foreach (var alias in commandAliases)
            {
                NativeCommands.UnregisterAlias(alias);
            }
            commandAliases.Clear();

            foreach (var aliasName in commandAliasNames)
            {
                CommandDispatcher.RemoveAlias(aliasName);
            }
            commandAliasNames.Clear();

            foreach (var callback in commandCallbacks)
            {
                CommandDispatcher.Unregister(callback);
            }
            commandCallbacks.Clear();
        }
    }

    private Guid AddCallback( CommandCallbackBase callback )
    {
        lock (commandLock)
        {
            commandCallbacks.Add(callback);
            CommandDispatcher.Register(callback);
        }

        return callback.Guid;
    }

    private void RemoveCallbacks( Predicate<CommandCallbackBase> match )
    {
        lock (commandLock)
        {
            foreach (var callback in commandCallbacks.FindAll(match))
            {
                CommandDispatcher.Unregister(callback);
            }

            _ = commandCallbacks.RemoveAll(match);
        }
    }

    private static CommandInfo ToCommandInfo( CommandCallback command ) => new() {
        CommandName = command.CommandName,
        RegisterRaw = command.RegisterRaw,
        Permission = command.Permission,
        HelpText = command.HelpText
    };
}
