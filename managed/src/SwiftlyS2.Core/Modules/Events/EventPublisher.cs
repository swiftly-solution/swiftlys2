using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.Events;

internal static class EventPublisher
{
    internal static event Action? InternalOnMapLoad;
    private static readonly List<EventSubscriber> subscribers = [];
    private static readonly Lock subscribersLock = new();

    public static void Subscribe( EventSubscriber subscriber )
    {
        lock (subscribersLock)
        {
            subscribers.Add(subscriber);
        }
    }

    public static void Unsubscribe( EventSubscriber subscriber )
    {
        lock (subscribersLock)
        {
            _ = subscribers.Remove(subscriber);
        }
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
            NativeEvents.RegisterOnStartupServerCallback((nint)(delegate* unmanaged< void >)&OnStartupServer);
            _ = NativeConvars.AddConvarCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConVarCreated);
            _ = NativeConvars.AddConCommandCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConCommandCreated);
            _ = NativeConvars.AddGlobalChangeListener((nint)(delegate* unmanaged< nint, int, nint, nint, void >)&OnConVarValueChanged);
            _ = NativeConsoleOutput.AddConsoleListener((nint)(delegate* unmanaged< nint, void >)&OnConsoleOutput);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarCreated( nint convarNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnConVarCreated @event = new() { ConVarName = Marshal.PtrToStringUTF8(convarNamePtr) ?? string.Empty };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnConVarCreated(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConCommandCreated( nint commandNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnConCommandCreated @event = new() { CommandName = Marshal.PtrToStringUTF8(commandNamePtr) ?? string.Empty };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnConCommandCreated(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarValueChanged( nint convarNamePtr, int playerid, nint newValuePtr, nint oldValuePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnConVarValueChanged @event = new() {
                PlayerId = playerid,
                ConVarName = Marshal.PtrToStringUTF8(convarNamePtr) ?? string.Empty,
                NewValue = Marshal.PtrToStringUTF8(newValuePtr) ?? string.Empty,
                OldValue = Marshal.PtrToStringUTF8(oldValuePtr) ?? string.Empty,
            };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnConVarValueChanged(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnTick( byte simulating, byte first, byte last )
    {
        SchedulerManager.OnTick();
        // CallbackDispatcher.RunFrame(true);

        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnTick();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPreworldUpdate( byte simulating )
    {
        SchedulerManager.OnWorldUpdate();

        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnWorldUpdate();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnClientConnected( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return 1;
        }

        try
        {
            OnClientConnectedEvent @event = new() { PlayerId = playerId };
            foreach (var subscriber in subscribers)
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
            if (!GlobalExceptionHandler.Handle(e))
            {
                return 1;
            }
            AnsiConsole.WriteException(e);
            return 1;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientDisconnected( int playerId, int reason )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnClientDisconnectedEvent @event = new() {
                PlayerId = playerId,
                Reason = (ENetworkDisconnectionReason)reason
            };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnClientDisconnected(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientKeyStateChanged( int playerId, GameButtons key, byte pressed )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnClientKeyStateChangedEvent @event = new() {
                PlayerId = playerId,
                Key = key.ToKeyKind(),
                Pressed = pressed != 0
            };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnClientKeyStateChanged(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientPutInServer( int playerId, int clientKind )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnClientPutInServerEvent @event = new() {
                PlayerId = playerId,
                Kind = (ClientKind)clientKind
            };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnClientPutInServer(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorize( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnClientSteamAuthorizeEvent @event = new() { PlayerId = playerId };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnClientSteamAuthorize(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorizeFail( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnClientSteamAuthorizeFailEvent @event = new() { PlayerId = playerId };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnClientSteamAuthorizeFail(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityCreated( nint entityPtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnEntityCreatedEvent @event = new() { Entity = new CEntityInstanceImpl(entityPtr) };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityCreated(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityDeleted( nint entityPtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnEntityDeletedEvent @event = new() { Entity = new CEntityInstanceImpl(entityPtr) };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityDeleted(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityParentChanged( nint entityPtr, nint newParentPtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnEntityParentChangedEvent @event = new() {
                Entity = new CEntityInstanceImpl(entityPtr),
                NewParent = newParentPtr != 0 ? new CEntityInstanceImpl(newParentPtr) : null
            };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityParentChanged(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntitySpawned( nint entityPtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnEntitySpawnedEvent @event = new() { Entity = new CEntityInstanceImpl(entityPtr) };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntitySpawned(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnMapLoad( nint mapNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            InternalOnMapLoad?.Invoke(); // calls before all plugins.
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }

        try
        {
            OnMapLoadEvent @event = new() { MapName = Marshal.PtrToStringUTF8(mapNamePtr) ?? string.Empty };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnMapLoad(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnMapUnload( nint mapNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnMapUnloadEvent @event = new() { MapName = Marshal.PtrToStringUTF8(mapNamePtr) ?? string.Empty };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnMapUnload(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientProcessUsercmds( int playerId, nint usercmdsPtr, int numcmds, byte paused, float margin )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            unsafe
            {
                var usercmdPtrs = new ReadOnlySpan<nint>(usercmdsPtr.ToPointer(), numcmds);
                List<CSGOUserCmdPB> usercmds = [];
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
                foreach (var subscriber in subscribers)
                {
                    subscriber.InvokeOnClientProcessUsercmds(@event);
                }
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnEntityTakeDamage( nint entityPtr, nint takeDamageInfoPtr, nint takeDamageResultPtr )
    {
        if (subscribers.Count == 0)
        {
            return 1;
        }

        try
        {
            unsafe
            {
                OnEntityTakeDamageEvent @event = new() {
                    Entity = new CEntityInstanceImpl(entityPtr),
                    _infoPtr = takeDamageInfoPtr,
                    _resultPtr = takeDamageResultPtr
                };
                foreach (var subscriber in subscribers)
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
            if (!GlobalExceptionHandler.Handle(e))
            {
                return 1;
            }
            AnsiConsole.WriteException(e);
            return 1;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPrecacheResource( nint pResourceManifest )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnPrecacheResourceEvent @event = new() { pResourceManifest = pResourceManifest };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnPrecacheResource(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnStartupServer()
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnStartupServer();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [Obsolete("InvokeOnEntityTouchHook is deprecated. Use InvokeOnEntityStartTouch, InvokeOnEntityTouch, or InvokeOnEntityEndTouch instead.")]
    public static void InvokeOnEntityTouchHook( OnEntityTouchHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityTouchHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnEntityStartTouch( OnEntityStartTouchEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityStartTouch(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnEntityTouch( OnEntityTouchEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityTouch(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnEntityEndTouch( OnEntityEndTouchEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityEndTouch(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnSteamAPIActivatedHook()
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnSteamAPIActivatedHook();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnCanAcquireHook( OnItemServicesCanAcquireHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnItemServicesCanAcquireHook(@event);
                if (@event.Intercepted)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnWeaponServicesCanUseHook( OnWeaponServicesCanUseHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnWeaponServicesCanUseHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConsoleOutput( nint messagePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnConsoleOutputEvent @event = new() { Message = Marshal.PtrToStringUTF8(messagePtr) ?? string.Empty };
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnConsoleOutput(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnCommandExecuteHook( OnCommandExecuteHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnCommandExecuteHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnMovementServicesRunCommandHook( OnMovementServicesRunCommandHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnMovementServicesRunCommandHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnPlayerPawnPostThinkHook( OnPlayerPawnPostThinkHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnPlayerPawnPostThinkHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnEntityIdentityAcceptInputHook( OnEntityIdentityAcceptInputHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityIdentityAcceptInputHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeEntityFireOutputHook( OnEntityFireOutputHookEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.InvokeOnEntityFireOutputHook(@event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }
}