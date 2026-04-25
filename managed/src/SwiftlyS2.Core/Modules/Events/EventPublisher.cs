using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.EntitySystem;

namespace SwiftlyS2.Core.Events;

internal static class EventPublisher
{
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
            NativeEvents.RegisterOnClientVoiceCallback((nint)(delegate* unmanaged< int, void >)&OnClientVoice);
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnConVarCreated(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnConCommandCreated(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnConVarValueChanged(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnTick();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnWorldUpdate();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnClientConnected( int playerId )
    {
        PlayerManagerService.RegisterPlayerObject(playerId);
        if (subscribers.Count == 0)
        {
            return 1;
        }

        try
        {
            OnClientConnectedEvent @event = new() { PlayerId = playerId };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientConnected(ref @event);

                if (@event.Result == HookResult.Handled)
                {
                    return 1;
                }

                if (@event.Result == HookResult.Stop)
                {
                    PlayerManagerService.UnregisterPlayerObject(playerId);
                    return 0;
                }
            }

            return 1;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientDisconnected(ref @event);
            }

            PlayerManagerService.UnregisterPlayerObject(playerId);

        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                PlayerManagerService.UnregisterPlayerObject(playerId);
                return;
            }

            PlayerManagerService.UnregisterPlayerObject(playerId);
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientKeyStateChanged(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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

        if (clientKind == (int)ClientKind.Bot) PlayerManagerService.RegisterPlayerObject(playerId);

        try
        {
            OnClientPutInServerEvent @event = new() {
                PlayerId = playerId,
                Kind = (ClientKind)clientKind
            };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientPutInServer(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientSteamAuthorize(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientSteamAuthorizeFail(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityCreated( nint entityPtr )
    {
        var entity = EntityManager.OnEntityCreated(entityPtr);
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnEntityCreatedEvent @event = new() { Entity = entity };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityCreated(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            EntityManager.OnEntityDeleted(entityPtr);
            return;
        }

        var entity = EntityManager.GetEntityByAddress(entityPtr);

        try
        {
            OnEntityDeletedEvent @event = new() { Entity = entity! };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityDeleted(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
        finally
        {
            EntityManager.OnEntityDeleted(entityPtr);
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
                Entity = EntityManager.GetEntityByAddress(entityPtr) ?? new CEntityInstanceImpl(entityPtr),
                NewParent = newParentPtr != 0 ? EntityManager.GetEntityByAddress(newParentPtr) ?? new CEntityInstanceImpl(newParentPtr) : null
            };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityParentChanged(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            OnEntitySpawnedEvent @event = new() { Entity = EntityManager.GetEntityByAddress(entityPtr) ?? new CEntityInstanceImpl(entityPtr) };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntitySpawned(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            OnMapLoadEvent @event = new() { MapName = Marshal.PtrToStringUTF8(mapNamePtr) ?? string.Empty };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnMapLoad(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientVoice( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            OnClientVoiceEvent @event = new() { PlayerId = playerId };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientVoice(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnMapUnload(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            List<CSGOUserCmdPB> usercmds = new(numcmds);

            unsafe
            {
                var usercmdPtrs = (nint*)usercmdsPtr;
                for (var i = 0; i < numcmds; i++)
                {
                    usercmds.Add(new CSGOUserCmdPBImpl(usercmdPtrs[i], false));
                }
            }

            OnClientProcessUsercmdsEvent @event = new() {
                PlayerId = playerId,
                Usercmds = usercmds,
                Paused = paused != 0,
                Margin = margin
            };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnClientProcessUsercmds(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
                if (takeDamageResultPtr == 0 && takeDamageInfoPtr != 0)
                {
                    var newTakeDamageResult = new CTakeDamageResult();
                    var damageInfo = (CTakeDamageInfo*)takeDamageInfoPtr;
                    newTakeDamageResult.OriginatingInfo = damageInfo;
                    newTakeDamageResult.HealthLost = (int)damageInfo->Damage;
                    newTakeDamageResult.DamageDealt = damageInfo->Damage;
                    newTakeDamageResult.PreModifiedDamage = damageInfo->Damage;
                    newTakeDamageResult.TotalledDamageDealt = damageInfo->Damage;
                    newTakeDamageResult.TotalledHealthLost = (int)damageInfo->Damage;
                    newTakeDamageResult.TotalledPreModifiedDamage = damageInfo->Damage;
                    newTakeDamageResult.DamageFlags = damageInfo->DamageFlags;
                    takeDamageResultPtr = (nint)(&newTakeDamageResult);
                }

                OnEntityTakeDamageEvent @event = new() {
                    Entity = EntityManager.GetEntityByAddress(entityPtr) ?? new CEntityInstanceImpl(entityPtr),
                    _infoPtr = takeDamageInfoPtr,
                    _resultPtr = takeDamageResultPtr
                };
                for (var i = 0; i < subscribers.Count; i++)
                {
                    subscribers[i].InvokeOnEntityTakeDamage(ref @event);

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
            if (GlobalExceptionHandler.Handle(ref e)) AnsiConsole.WriteException(e);

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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnPrecacheResource(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnStartupServer();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityStartTouch(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityTouch(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityEndTouch(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnSteamAPIActivatedHook();
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnItemServicesCanAcquireHook(ref @event);
                if (@event.Intercepted)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnWeaponServicesCanUseHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            OnConsoleOutputEvent @event = new() { Message = StringAlloc.CreateCSharpString(messagePtr) };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnConsoleOutput(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnCommandExecuteHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnMovementServicesRunCommandHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnPlayerPawnPostThinkHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityIdentityAcceptInputHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    public static void InvokeOnWeaponServicesDropWeaponHook( OnWeaponServicesDropWeaponHook @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnWeaponServicesDropWeaponHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
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
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityFireOutputHook(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }
}