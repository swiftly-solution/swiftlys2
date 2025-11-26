using System.Runtime.InteropServices;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Permissions;
using Microsoft.Extensions.Logging;

namespace SwiftlyS2.Core.Commands;

internal delegate void CommandCallbackDelegate( int playerId, nint args, nint commandName, nint prefix, byte slient );

internal delegate HookResult ClientCommandListenerCallbackDelegate( int playerId, nint commandLine );

internal delegate HookResult ClientChatListenerCallbackDelegate( int playerId, nint text, byte teamonly );

internal abstract class CommandCallbackBase : IDisposable
{
    public Guid Guid { get; protected init; }
    public IContextedProfilerService Profiler { get; }
    public ILoggerFactory LoggerFactory { get; }

    protected CommandCallbackBase( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        LoggerFactory = loggerFactory;
        Profiler = profiler;
    }

    public abstract void Dispose();
}

internal class CommandCallback : CommandCallbackBase
{
    public string CommandName { get; protected init; }

    private readonly ICommandService.CommandListener commandHandle;
    private readonly CommandCallbackDelegate commandCallback;

    private readonly nint commandCallbackPtr;
    private readonly string commandPermissions;
    private readonly ulong nativeListenerId;
    private readonly ILogger<CommandCallback> logger;

    public CommandCallback( string commandName, bool registerRaw, ICommandService.CommandListener handler, string permission, IPlayerManagerService playerManagerService, IPermissionManager permissionManager, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        this.logger = LoggerFactory.CreateLogger<CommandCallback>();

        Guid = Guid.NewGuid();

        CommandName = commandName;
        commandPermissions = permission;
        commandHandle = handler;
        commandCallback = ( playerId, argsPtr, commandNamePtr, prefixPtr, slient ) =>
        {
            try
            {
                var category = "CommandCallback::" + CommandName;
                Profiler.StartRecording(category);
                var argsString = Marshal.PtrToStringUTF8(argsPtr)!;
                var commandNameString = Marshal.PtrToStringUTF8(commandNamePtr)!;
                var prefixString = Marshal.PtrToStringUTF8(prefixPtr)!;

                var args = argsString.Split('\x01');
                var context = new CommandContext(playerId, args, commandNameString, prefixString, slient == 1);
                if (!context.IsSentByPlayer || string.IsNullOrWhiteSpace(commandPermissions) || permissionManager.PlayerHasPermission(playerManagerService.GetPlayer(playerId).SteamID, commandPermissions))
                {
                    commandHandle(context);
                }
                else
                {
                    context.Reply("You do not have permission to use this command.");
                }
                Profiler.StopRecording(category);
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return;
                logger.LogError(e, "Failed to handle command {CommandName}.", commandName);
            }
        };

        commandCallbackPtr = Marshal.GetFunctionPointerForDelegate(commandCallback);
        nativeListenerId = NativeCommands.RegisterCommand(commandName, commandCallbackPtr, registerRaw);
    }

    public override void Dispose()
    {
        NativeCommands.UnregisterCommand(nativeListenerId);
    }
}

internal class ClientCommandListenerCallback : CommandCallbackBase
{
    private readonly ICommandService.ClientCommandHandler commandHandle;
    private readonly ClientCommandListenerCallbackDelegate commandCallback;
    private readonly nint commandCallbackPtr;
    private readonly ulong nativeListenerId;
    private readonly ILogger<ClientCommandListenerCallback> logger;

    public ClientCommandListenerCallback( ICommandService.ClientCommandHandler handler, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        logger = LoggerFactory.CreateLogger<ClientCommandListenerCallback>();
        Guid = Guid.NewGuid();

        commandHandle = handler;
        commandCallback = ( playerId, commandLinePtr ) =>
        {
            try
            {
                var category = "ClientCommandListenerCallback";
                Profiler.StartRecording(category);
                var commandLineString = Marshal.PtrToStringUTF8(commandLinePtr)!;
                var result = commandHandle(playerId, commandLineString);
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
                logger.LogError(e, "Failed to handle client command listener.");
                return HookResult.Continue;
            }
        };

        commandCallbackPtr = Marshal.GetFunctionPointerForDelegate(commandCallback);
        nativeListenerId = NativeCommands.RegisterClientCommandsListener(commandCallbackPtr);
    }

    public override void Dispose()
    {
        NativeCommands.UnregisterClientCommandsListener(nativeListenerId);
    }
}

internal class ClientChatListenerCallback : CommandCallbackBase
{
    private readonly ICommandService.ClientChatHandler commandHandle;
    private readonly ClientChatListenerCallbackDelegate commandCallback;
    private readonly nint commandCallbackPtr;
    private readonly ulong nativeListenerId;
    private readonly ILogger<ClientChatListenerCallback> logger;

    public ClientChatListenerCallback( ICommandService.ClientChatHandler handler, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        logger = LoggerFactory.CreateLogger<ClientChatListenerCallback>();
        Guid = Guid.NewGuid();

        commandHandle = handler;
        commandCallback = ( playerId, textPtr, teamonly ) =>
        {
            try
            {
                var category = "ClientChatListenerCallback";
                Profiler.StartRecording(category);
                var textString = Marshal.PtrToStringUTF8(textPtr)!;
                var result = commandHandle(playerId, textString, teamonly == 1);
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
                logger.LogError(e, "Failed to handle client chat listener.");
                return HookResult.Continue;
            }
        };

        commandCallbackPtr = Marshal.GetFunctionPointerForDelegate(commandCallback);
        nativeListenerId = NativeCommands.RegisterClientChatListener(commandCallbackPtr);
    }

    public override void Dispose()
    {
        NativeCommands.UnregisterClientChatListener(nativeListenerId);
    }
}