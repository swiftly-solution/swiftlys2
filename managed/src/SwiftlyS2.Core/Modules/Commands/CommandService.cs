using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Core.Models;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Misc;
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
    private static readonly Dictionary<string, List<CommandCallbackBase>> commandsByPlugin = [];
    private static readonly Dictionary<string, string> aliasToOriginal = [];
    private readonly Lock commandLock = new();

    private static readonly Lock dispatchLock = new();

    public CommandService( ILoggerFactory loggerFactory, IContextedProfilerService profiler, IPlayerManagerService playerManagerService, IPermissionManager permissionManager, IOptionsMonitor<CommandOverrideConfig> commandOverrideOptions, CoreContext coreContext )
    {
        this.loggerFactory = loggerFactory;
        this.profiler = profiler;
        this.playerManagerService = playerManagerService;
        this.permissionManager = permissionManager;
        this.commandOverrideOptions = commandOverrideOptions;
        this.coreContext = coreContext;

        lock (commandLock)
        {
            commandCallbacks.Clear();
            commandAliases.Clear();
        }

    }

    public static void DispatchCommand( string commandName, int playerId, string[] args, string originalCommandName, string prefix, bool silent )
    {
        lock (dispatchLock)
        {
            var resolvedName = aliasToOriginal.TryGetValue(commandName, out var original) ? original : commandName;
            foreach (var pluginCallbacks in commandsByPlugin.Values)
            {
                foreach (var cb in pluginCallbacks)
                {
                    if (cb is not CommandCallback cc) continue;
                    var normalizedName = cc.RegisterRaw ? cc.CommandName : "sw_" + cc.CommandName;
                    if (string.Equals(normalizedName, resolvedName, StringComparison.OrdinalIgnoreCase))
                    {
                        cc.Invoke(playerId, args, originalCommandName, prefix, silent);
                    }
                }
            }
        }
    }

    public Guid RegisterCommand( string commandName, ICommandService.CommandListener handler, bool registerRaw, string permission )
    {
        return RegisterCommand(commandName, handler, registerRaw, permission, "SwiftlyS2 registered command");
    }

    public Guid RegisterCommand( string commandName, ICommandService.CommandListener handler, bool registerRaw = false, string permission = "", string helpText = "SwiftlyS2 registered command" )
    {
        var callback = new CommandCallback(commandName, registerRaw, handler, permission, helpText, playerManagerService, permissionManager, commandOverrideOptions, loggerFactory, profiler, coreContext.Name);
        lock (commandLock)
        {
            commandCallbacks.Add(callback);

            if (!commandsByPlugin.TryGetValue(coreContext.Name, out var value))
            {
                value = [];
                commandsByPlugin[coreContext.Name] = value;
            }

            value.Add(callback);
        }
        return callback.Guid;
    }

    public void RegisterCommandAlias( string commandName, string alias, bool registerRaw = false )
    {
        lock (commandLock)
        {
            var commandId = NativeCommands.RegisterAlias(alias, commandName, registerRaw);
            if (commandId != 0)
            {
                commandAliases.Add(commandId);

                var normalizedAlias = registerRaw ? alias.ToLower() : "sw_" + alias.ToLower();
                var originalCallback = commandsByPlugin.Values
                    .SelectMany(x => x)
                    .OfType<CommandCallback>()
                    .FirstOrDefault(cc =>
                        cc.CommandName.Equals(commandName, StringComparison.OrdinalIgnoreCase) ||
                        ("sw_" + cc.CommandName).Equals(commandName, StringComparison.OrdinalIgnoreCase));

                if (originalCallback != null)
                {
                    var normalizedOriginal = originalCallback.RegisterRaw ? originalCallback.CommandName : "sw_" + originalCallback.CommandName;
                    aliasToOriginal[normalizedAlias] = normalizedOriginal;
                    commandAliasNames.Add(normalizedAlias);
                }
            }
        }
    }

    public void UnregisterCommand( Guid guid )
    {
        lock (commandLock)
        {
            _ = commandCallbacks.RemoveAll(callback =>
            {
                if (callback.Guid == guid)
                {
                    callback.Dispose();

                    if (commandsByPlugin.TryGetValue(callback.PluginName, out var pluginCallbacks))
                    {
                        _ = pluginCallbacks.Remove(callback);
                        if (pluginCallbacks.Count == 0)
                        {
                            _ = commandsByPlugin.Remove(callback.PluginName);
                        }
                    }

                    return true;
                }
                return false;
            });
        }
    }

    public void UnregisterCommand( string commandName )
    {
        lock (commandLock)
        {
            _ = commandCallbacks.RemoveAll(callback =>
            {
                if (callback is CommandCallback commandCallback && commandCallback.CommandName == commandName)
                {
                    commandCallback.Dispose();

                    if (commandsByPlugin.TryGetValue(callback.PluginName, out var pluginCallbacks))
                    {
                        _ = pluginCallbacks.Remove(callback);
                        if (pluginCallbacks.Count == 0)
                        {
                            _ = commandsByPlugin.Remove(callback.PluginName);
                        }
                    }

                    return true;
                }
                return false;
            });
        }
    }

    public bool IsCommandRegistered( string commandName )
    {
        return NativeCommands.IsCommandRegistered(commandName);
    }

    public static int DispatchClientCommand( int playerId, string commandLine )
    {
        lock (dispatchLock)
        {
            var stopOriginal = false;
            foreach (var pluginCallbacks in commandsByPlugin.Values)
            {
                foreach (var cb in pluginCallbacks)
                {
                    if (cb is not ClientCommandListenerCallback cc) continue;
                    var result = cc.Invoke(playerId, commandLine);
                    if (result == HookResult.Stop)
                        return (int)HookResult.Stop;
                    if (result == HookResult.Handled)
                        return (int)HookResult.Handled;
                    if (result == HookResult.CancelOriginal)
                        stopOriginal = true;
                }
            }
            return stopOriginal ? (int)HookResult.CancelOriginal : (int)HookResult.Continue;
        }
    }

    public Guid HookClientCommand( ICommandService.ClientCommandHandler handler )
    {
        var callback = new ClientCommandListenerCallback(handler, loggerFactory, profiler, coreContext.Name);
        lock (commandLock)
        {
            commandCallbacks.Add(callback);

            if (!commandsByPlugin.TryGetValue(coreContext.Name, out var value))
            {
                value = [];
                commandsByPlugin[coreContext.Name] = value;
            }

            value.Add(callback);
        }
        return callback.Guid;
    }

    public void UnhookClientCommand( Guid guid )
    {
        lock (commandLock)
        {
            _ = commandCallbacks.RemoveAll(callback =>
            {
                if (callback is ClientCommandListenerCallback clientCommandCallback && clientCommandCallback.Guid == guid)
                {
                    clientCommandCallback.Dispose();

                    if (commandsByPlugin.TryGetValue(callback.PluginName, out var pluginCallbacks))
                    {
                        _ = pluginCallbacks.Remove(callback);
                        if (pluginCallbacks.Count == 0)
                        {
                            _ = commandsByPlugin.Remove(callback.PluginName);
                        }
                    }

                    return true;
                }
                return false;
            });
        }
    }

    public static int DispatchClientChat( int playerId, string text, bool teamonly )
    {
        lock (dispatchLock)
        {
            var stopOriginal = false;
            foreach (var pluginCallbacks in commandsByPlugin.Values)
            {
                foreach (var cb in pluginCallbacks)
                {
                    if (cb is not ClientChatListenerCallback cc) continue;
                    var result = cc.Invoke(playerId, text, teamonly);
                    if (result == HookResult.Stop)
                        return (int)HookResult.Stop;
                    if (result == HookResult.Handled)
                        return (int)HookResult.Handled;
                    if (result == HookResult.CancelOriginal)
                        stopOriginal = true;
                }
            }
            return stopOriginal ? (int)HookResult.CancelOriginal : (int)HookResult.Continue;
        }
    }

    public Guid HookClientChat( ICommandService.ClientChatHandler handler )
    {
        var callback = new ClientChatListenerCallback(handler, loggerFactory, profiler, coreContext.Name);
        lock (commandLock)
        {
            commandCallbacks.Add(callback);

            if (!commandsByPlugin.TryGetValue(coreContext.Name, out var value))
            {
                value = [];
                commandsByPlugin[coreContext.Name] = value;
            }

            value.Add(callback);
        }
        return callback.Guid;
    }

    public void UnhookClientChat( Guid guid )
    {
        lock (commandLock)
        {
            _ = commandCallbacks.RemoveAll(callback =>
            {
                if (callback is ClientChatListenerCallback clientChatListenerCallback && clientChatListenerCallback.Guid == guid)
                {
                    clientChatListenerCallback.Dispose();

                    if (commandsByPlugin.TryGetValue(callback.PluginName, out var pluginCallbacks))
                    {
                        _ = pluginCallbacks.Remove(callback);
                        if (pluginCallbacks.Count == 0)
                        {
                            _ = commandsByPlugin.Remove(callback.PluginName);
                        }
                    }

                    return true;
                }
                return false;
            });
        }
    }

    public List<string> GetAllCommands()
    {
        var commandNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        lock (commandLock)
        {
            foreach (var callbacks in commandsByPlugin.Values)
            {
                foreach (var callback in callbacks)
                {
                    if (callback is CommandCallback commandCallback)
                        _ = commandNames.Add(commandCallback.CommandName);
                }
            }
        }
        return commandNames.ToList();
    }

    public void Dispose()
    {
        lock (commandLock)
        {
            foreach (var alias in commandAliases)
                NativeCommands.UnregisterAlias(alias);
            commandAliases.Clear();

            foreach (var aliasName in commandAliasNames)
                _ = aliasToOriginal.Remove(aliasName);
            commandAliasNames.Clear();

            foreach (var callback in commandCallbacks)
                callback.Dispose();
            commandCallbacks.Clear();

            if(commandsByPlugin.TryGetValue(coreContext.Name, out var pluginCallbacks))
            {
                pluginCallbacks.Clear();
            }
        }
    }

    public List<CommandInfo> GetCommandsByPlugin( string pluginName )
    {
        lock (commandLock)
        {
            return commandsByPlugin.TryGetValue(pluginName, out var callbacks)
                ? callbacks.OfType<CommandCallback>().Select(c => new CommandInfo {
                    CommandName = c.CommandName,
                    RegisterRaw = c.RegisterRaw,
                    Permission = c.Permission,
                    HelpText = c.HelpText
                }).ToList()
                : [];
        }
    }

    public Dictionary<string, List<CommandInfo>> GetAllCommandsByPlugin()
    {
        lock (commandLock)
        {
            return commandsByPlugin.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value
                    .OfType<CommandCallback>()
                    .Select(c => new CommandInfo {
                        CommandName = c.CommandName,
                        RegisterRaw = c.RegisterRaw,
                        Permission = c.Permission,
                        HelpText = c.HelpText
                    })
                    .ToList()
            );
        }
    }

    public List<CommandInfo> GetAllCommandsInfo()
    {
        lock (commandLock)
        {
            return commandsByPlugin
                .Values
                .SelectMany(callbacks => callbacks.OfType<CommandCallback>())
                .Select(c => new CommandInfo {
                    CommandName = c.CommandName,
                    RegisterRaw = c.RegisterRaw,
                    Permission = c.Permission,
                    HelpText = c.HelpText
                })
                .ToList();
        }
    }
}