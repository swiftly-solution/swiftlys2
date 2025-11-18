using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Permissions;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Profiler;

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

    private readonly ICommandService.CommandListener _handler;
    private readonly CommandCallbackDelegate _unmanagedCallback;

    private readonly nint _unmanagedCallbackPtr;
    private readonly ulong _nativeListenerId;
    private readonly string _permissions;

    private readonly ILogger<CommandCallback> _logger;
    private readonly IPlayerManagerService _playerManagerService;
    private readonly IPermissionManager _permissionManager;

    public CommandCallback( string commandName, bool registerRaw, ICommandService.CommandListener handler, string permission, IPlayerManagerService playerManagerService, IPermissionManager permissionManager, ILoggerFactory loggerFactory, IContextedProfilerService profiler )
      : base(loggerFactory, profiler)
    {
        _logger = LoggerFactory.CreateLogger<CommandCallback>();
        _playerManagerService = playerManagerService;
        _permissionManager = permissionManager;
        Guid = Guid.NewGuid();

        CommandName = commandName;
        _permissions = permission;
        _handler = handler;

        _unmanagedCallback = ( playerId, argsPtr, commandNamePtr, prefixPtr, slient ) =>
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
                if (!context.IsSentByPlayer || string.IsNullOrWhiteSpace(_permissions) || _permissionManager.PlayerHasPermission(_playerManagerService.GetPlayer(playerId).SteamID, _permissions))
                {
                    _handler(context);
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
                _logger.LogError(e, "Failed to handle command {0}.", commandName);
            }
        };

        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);

        _nativeListenerId = NativeCommands.RegisterCommand(commandName, _unmanagedCallbackPtr, registerRaw);
    }

    public override void Dispose()
    {
        NativeCommands.UnregisterCommand(_nativeListenerId);
    }
}

internal class ClientCommandListenerCallback : CommandCallbackBase
{

    private readonly ICommandService.ClientCommandHandler _handler;
    private readonly ClientCommandListenerCallbackDelegate _unmanagedCallback;
    private readonly nint _unmanagedCallbackPtr;
    private readonly ulong _nativeListenerId;
    private readonly ILogger<ClientCommandListenerCallback> _logger;

    public ClientCommandListenerCallback( ICommandService.ClientCommandHandler handler, ILoggerFactory loggerFactory, IContextedProfilerService profiler )
      : base(loggerFactory, profiler)
    {
        _logger = LoggerFactory.CreateLogger<ClientCommandListenerCallback>();
        Guid = Guid.NewGuid();

        _handler = handler;

        _unmanagedCallback = ( playerId, commandLinePtr ) =>
        {
            try
            {
                var category = "ClientCommandListenerCallback";
                Profiler.StartRecording(category);
                var commandLineString = Marshal.PtrToStringUTF8(commandLinePtr)!;
                var result = _handler(playerId, commandLineString);
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
                _logger.LogError(e, "Failed to handle client command listener.");
                return HookResult.Continue;
            }
        };

        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);

        _nativeListenerId = NativeCommands.RegisterClientCommandsListener(_unmanagedCallbackPtr);

    }

    public override void Dispose()
    {
        NativeCommands.UnregisterClientCommandsListener(_nativeListenerId);
    }
}

internal class ClientChatListenerCallback : CommandCallbackBase
{

    private readonly ICommandService.ClientChatHandler _handler;
    private readonly ClientChatListenerCallbackDelegate _unmanagedCallback;
    private readonly nint _unmanagedCallbackPtr;
    private readonly ulong _nativeListenerId;
    private readonly ILogger<ClientChatListenerCallback> _logger;

    public ClientChatListenerCallback( ICommandService.ClientChatHandler handler, ILoggerFactory loggerFactory, IContextedProfilerService profiler )
      : base(loggerFactory, profiler)
    {
        _logger = LoggerFactory.CreateLogger<ClientChatListenerCallback>();
        Guid = Guid.NewGuid();

        _handler = handler;

        _unmanagedCallback = ( playerId, textPtr, teamonly ) =>
        {
            try
            {
                var category = "ClientChatListenerCallback";
                Profiler.StartRecording(category);
                var textString = Marshal.PtrToStringUTF8(textPtr)!;
                var result = _handler(playerId, textString, teamonly == 1);
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
                _logger.LogError(e, "Failed to handle client chat listener.");
                return HookResult.Continue;
            }
        };

        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);

        _nativeListenerId = NativeCommands.RegisterClientChatListener(_unmanagedCallbackPtr);

    }

    public override void Dispose()
    {
        NativeCommands.UnregisterClientChatListener(_nativeListenerId);
    }
}