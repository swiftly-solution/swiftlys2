using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
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

    private readonly List<CommandCallbackBase> commandCallbacks = [];
    private readonly List<ulong> commandAliases = [];
    private readonly Lock commandLock = new();

    public CommandService( ILoggerFactory loggerFactory, IContextedProfilerService profiler, IPlayerManagerService playerManagerService, IPermissionManager permissionManager )
    {
        this.loggerFactory = loggerFactory;
        this.profiler = profiler;
        this.playerManagerService = playerManagerService;
        this.permissionManager = permissionManager;

        lock (commandLock)
        {
            commandCallbacks.Clear();
            commandAliases.Clear();
        }
    }

    public Guid RegisterCommand( string commandName, ICommandService.CommandListener handler, bool registerRaw = false, string permission = "" )
    {
        var callback = new CommandCallback(commandName, registerRaw, handler, permission, playerManagerService, permissionManager, loggerFactory, profiler);
        lock (commandLock)
        {
            commandCallbacks.Add(callback);
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
                    return true;
                }
                return false;
            });
        }
    }

    public Guid HookClientCommand( ICommandService.ClientCommandHandler handler )
    {
        var callback = new ClientCommandListenerCallback(handler, loggerFactory, profiler);
        lock (commandLock)
        {
            commandCallbacks.Add(callback);
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
                    return true;
                }
                return false;
            });
        }
    }

    public Guid HookClientChat( ICommandService.ClientChatHandler handler )
    {
        var callback = new ClientChatListenerCallback(handler, loggerFactory, profiler);
        lock (commandLock)
        {
            commandCallbacks.Add(callback);
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
                    return true;
                }
                return false;
            });
        }
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

            foreach (var callback in commandCallbacks)
            {
                callback.Dispose();
            }
            commandCallbacks.Clear();
        }
    }
}