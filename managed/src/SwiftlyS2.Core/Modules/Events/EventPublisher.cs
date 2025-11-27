using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using Spectre.Console;
using System.Runtime.CompilerServices;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.SteamAPI;

namespace SwiftlyS2.Core.Events;

internal static class EventPublisher
{

    private static List<EventSubscriber> _subscribers = new();

    public static void Subscribe( EventSubscriber subscriber )
    {
        _subscribers.Add(subscriber);
    }
    public static void Unsubscribe( EventSubscriber subscriber )
    {
        _subscribers.Remove(subscriber);
    }

    public static void Register()
    {
        unsafe
        {
            NativeEvents.RegisterOnGameTickCallback((nint)(delegate* unmanaged< byte, byte, byte, void >)&OnTick);
            NativeEvents.RegisterOnPreworldUpdateCallback((nint)(delegate* unmanaged< byte, void >)&OnPreworldUpdate);
            NativeEvents.RegisterOnClientConnectCallback((nint)(delegate* unmanaged< int, byte >)&OnClientConnected);
            NativeEvents.RegisterOnClientDisconnectCallback((nint)(delegate* unmanaged< int, int, void >)&OnClientDisconnected);
            NativeEvents.RegisterOnClientKeyStateChangedCallback((nint)(delegate* unmanaged< int, GameButtons, byte, void >)&OnClientKeyStateChanged);
            NativeEvents.RegisterOnClientPutInServerCallback((nint)(delegate* unmanaged< int, int, void >)&OnClientPutInServer);
            NativeEvents.RegisterOnClientSteamAuthorizeCallback((nint)(delegate* unmanaged< int, void >)&OnClientSteamAuthorize);
            NativeEvents.RegisterOnClientSteamAuthorizeFailCallback((nint)(delegate* unmanaged< int, void >)&OnClientSteamAuthorizeFail);
            NativeEvents.RegisterOnEntityCreatedCallback((nint)(delegate* unmanaged< nint, void >)&OnEntityCreated);
            NativeEvents.RegisterOnEntityDeletedCallback((nint)(delegate* unmanaged< nint, void >)&OnEntityDeleted);
            NativeEvents.RegisterOnEntityParentChangedCallback((nint)(delegate* unmanaged< nint, nint, void >)&OnEntityParentChanged);
            NativeEvents.RegisterOnEntitySpawnedCallback((nint)(delegate* unmanaged< nint, void >)&OnEntitySpawned);
            NativeEvents.RegisterOnMapLoadCallback((nint)(delegate* unmanaged< nint, void >)&OnMapLoad);
            NativeEvents.RegisterOnMapUnloadCallback((nint)(delegate* unmanaged< nint, void >)&OnMapUnload);
            NativeEvents.RegisterOnClientProcessUsercmdsCallback((nint)(delegate* unmanaged< int, nint, int, byte, float, void >)&OnClientProcessUsercmds);
            NativeEvents.RegisterOnEntityTakeDamageCallback((nint)(delegate* unmanaged< nint, nint, nint, byte >)&OnEntityTakeDamage);
            NativeEvents.RegisterOnPrecacheResourceCallback((nint)(delegate* unmanaged< nint, void >)&OnPrecacheResource);
            NativeConvars.AddConvarCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConVarCreated);
            NativeConvars.AddConCommandCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConCommandCreated);
            NativeConvars.AddGlobalChangeListener((nint)(delegate* unmanaged< nint, int, nint, nint, void >)&OnConVarValueChanged);
            NativeConsoleOutput.AddConsoleListener((nint)(delegate* unmanaged< nint, void >)&OnConsoleOutput);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarCreated( nint convarNamePtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            string convarName = Marshal.PtrToStringUTF8(convarNamePtr) ?? string.Empty;
            OnConVarCreated @event = new() {
                ConVarName = convarName
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnConVarCreated(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConCommandCreated( nint commandNamePtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            string commandName = Marshal.PtrToStringUTF8(commandNamePtr) ?? string.Empty;
            OnConCommandCreated @event = new() {
                CommandName = commandName
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnConCommandCreated(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarValueChanged( nint convarNamePtr, int playerid, nint newValuePtr, nint oldValuePtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            string convarName = Marshal.PtrToStringUTF8(convarNamePtr) ?? string.Empty;
            string newValue = Marshal.PtrToStringUTF8(newValuePtr) ?? string.Empty;
            string oldValue = Marshal.PtrToStringUTF8(oldValuePtr) ?? string.Empty;
            OnConVarValueChanged @event = new() {
                ConVarName = convarName,
                PlayerId = playerid,
                NewValue = newValue,
                OldValue = oldValue
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnConVarValueChanged(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnTick( byte simulating, byte first, byte last )
    {
        SchedulerManager.OnTick();
        // CallbackDispatcher.RunFrame(true);
        if (_subscribers.Count == 0) return;
        try
        {
            _subscribers.ForEach(subscriber => subscriber.InvokeOnTick());
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPreworldUpdate( byte simulating )
    {
        SchedulerManager.OnWorldUpdate();
        if (_subscribers.Count == 0) return;
        try
        {
            _subscribers.ForEach(subscriber => subscriber.InvokeOnWorldUpdate());
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnClientConnected( int playerId )
    {
        if (_subscribers.Count == 0) return 1;
        try
        {
            OnClientConnectedEvent @event = new() {
                PlayerId = playerId
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnClientConnected(@event);

                if (@event.Result == HookResult.Handled)
                {
                    return 1;
                }

                if (@event.Result == HookResult.Stop)
                {
                    return 0;
                }
            }

            return 1;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return 1;
            AnsiConsole.WriteException(e);
            return 1;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientDisconnected( int playerId, int reason )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnClientDisconnectedEvent @event = new() {
                PlayerId = playerId,
                Reason = (ENetworkDisconnectionReason)reason
            };

            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnClientDisconnected(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientKeyStateChanged( int playerId, GameButtons key, byte pressed )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnClientKeyStateChangedEvent @event = new() {
                PlayerId = playerId,
                Key = key.ToKeyKind(),
                Pressed = pressed != 0
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnClientKeyStateChanged(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientPutInServer( int playerId, int clientKind )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnClientPutInServerEvent @event = new() {
                PlayerId = playerId,
                Kind = (ClientKind)clientKind
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnClientPutInServer(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorize( int playerId )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnClientSteamAuthorizeEvent @event = new() {
                PlayerId = playerId
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnClientSteamAuthorize(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorizeFail( int playerId )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnClientSteamAuthorizeFailEvent @event = new() {
                PlayerId = playerId
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnClientSteamAuthorizeFail(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityCreated( nint entityPtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            var entity = new CEntityInstanceImpl(entityPtr);
            OnEntityCreatedEvent @event = new() {
                Entity = entity
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityCreated(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityDeleted( nint entityPtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            var entity = new CEntityInstanceImpl(entityPtr);
            OnEntityDeletedEvent @event = new() {
                Entity = entity
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityDeleted(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityParentChanged( nint entityPtr, nint newParentPtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            var entity = new CEntityInstanceImpl(entityPtr);
            CEntityInstance? parent = newParentPtr != 0 ? new CEntityInstanceImpl(newParentPtr) : null;
            OnEntityParentChangedEvent @event = new() {
                Entity = entity,
                NewParent = parent
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityParentChanged(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntitySpawned( nint entityPtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            var entity = new CEntityInstanceImpl(entityPtr);
            OnEntitySpawnedEvent @event = new() {
                Entity = entity
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntitySpawned(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnMapLoad( nint mapNamePtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            string map = Marshal.PtrToStringUTF8(mapNamePtr) ?? string.Empty;
            OnMapLoadEvent @event = new() {
                MapName = map
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnMapLoad(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnMapUnload( nint mapNamePtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            string map = Marshal.PtrToStringUTF8(mapNamePtr) ?? string.Empty;
            OnMapUnloadEvent @event = new() {
                MapName = map
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnMapUnload(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientProcessUsercmds( int playerId, nint usercmdsPtr, int numcmds, byte paused, float margin )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            unsafe
            {
                ReadOnlySpan<nint> usercmdPtrs = new ReadOnlySpan<nint>(usercmdsPtr.ToPointer(), numcmds);
                List<CSGOUserCmdPB> usercmds = new();
                foreach (var pUsercmd in usercmdPtrs)
                {
                    var usercmd = new CSGOUserCmdPBImpl(pUsercmd, false);
                    usercmds.Add(usercmd);
                }

                OnClientProcessUsercmdsEvent @event = new() {
                    PlayerId = playerId,
                    Usercmds = usercmds,
                    Paused = paused != 0,
                    Margin = margin
                };
                foreach (var subscriber in _subscribers)
                {
                    subscriber.InvokeOnClientProcessUsercmds(@event);
                }
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnEntityTakeDamage( nint entityPtr, nint takeDamageInfoPtr, nint takeDamageResultPtr )
    {
        if (_subscribers.Count == 0) return 1;
        try
        {
            unsafe
            {
                var entity = new CEntityInstanceImpl(entityPtr);
                OnEntityTakeDamageEvent @event = new() {
                    Entity = entity,
                    _infoPtr = takeDamageInfoPtr,
                    _resultPtr = takeDamageResultPtr
                };
                foreach (var subscriber in _subscribers)
                {
                    subscriber.InvokeOnEntityTakeDamage(@event);

                    if (@event.Result == HookResult.Handled)
                    {
                        return 1;
                    }

                    if (@event.Result == HookResult.Stop)
                    {
                        return 0;
                    }
                }
                return 1;
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return 1;
            AnsiConsole.WriteException(e);
            return 1;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPrecacheResource( nint pResourceManifest )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnPrecacheResourceEvent @event = new() {
                pResourceManifest = pResourceManifest
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnPrecacheResource(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [Obsolete("InvokeOnEntityTouchHook is deprecated. Use InvokeOnEntityStartTouch, InvokeOnEntityTouch, or InvokeOnEntityEndTouch instead.")]
    public static void InvokeOnEntityTouchHook( OnEntityTouchHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityTouchHook(@event);
            }
            return;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnEntityStartTouch( OnEntityStartTouchEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityStartTouch(@event);
            }
            return;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnEntityTouch( OnEntityTouchEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityTouch(@event);
            }
            return;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnEntityEndTouch( OnEntityEndTouchEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityEndTouch(@event);
            }
            return;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnSteamAPIActivatedHook()
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnSteamAPIActivatedHook();
            }
            return;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnCanAcquireHook( OnItemServicesCanAcquireHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnItemServicesCanAcquireHook(@event);
                if (@event.Intercepted)
                {
                    return;
                }
            }
            return;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnWeaponServicesCanUseHook( OnWeaponServicesCanUseHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnWeaponServicesCanUseHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConsoleOutput( nint messagePtr )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            OnConsoleOutputEvent @event = new() {
                Message = Marshal.PtrToStringUTF8(messagePtr) ?? string.Empty
            };
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnConsoleOutput(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnCommandExecuteHook( OnCommandExecuteHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnCommandExecuteHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnMovementServicesRunCommandHook( OnMovementServicesRunCommandHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnMovementServicesRunCommandHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
            return;
        }
    }

    public static void InvokeOnPlayerPawnPostThinkHook( OnPlayerPawnPostThinkHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnPlayerPawnPostThinkHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnEntityIdentityAcceptInputHook( OnEntityIdentityAcceptInputHookEvent @event )
    {
        if (_subscribers.Count == 0) return;
        try
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.InvokeOnEntityIdentityAcceptInputHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }
}