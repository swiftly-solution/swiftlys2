using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Commands;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Core.GameHooks;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Core.Convars;

namespace SwiftlyS2.Core.Events;

internal enum CoreEvent
{
    Tick,
    WorldUpdate,
    ClientConnected,
    ClientDisconnected,
    ClientKeyStateChanged,
    ClientPutInServer,
    ClientSteamAuthorize,
    ClientSteamAuthorizeFail,
    EntityCreated,
    EntityDeleted,
    EntityParentChanged,
    EntitySpawned,
    MapLoad,
    MapUnload,
    ClientProcessUsercmds,
    ConVarValueChanged,
    ConCommandCreated,
    ConVarCreated,
    EntityTakeDamage,
    PrecacheResource,
    EntityStartTouch,
    EntityTouch,
    EntityEndTouch,
    CanAcquire,
    CanUseWeapon,
    WeaponDrop,
    ConsoleOutput,
    CommandExecuteHook,
    SteamAPIActivated,
    RunCommand,
    PostThink,
    AcceptInput,
    FireOutput,
    StartupServer,
    ClientVoice,
    ClientSettingsChanged,
    CustomHudClicked,
}

internal static class EventPublisher
{
    private static readonly ulong[] nativeListenerIds = new ulong[Enum.GetValues<CoreEvent>().Length];

    /// <summary>
    /// Per-event subscriber lists. EventSubscriber adds itself on its first handler for an event and removes itself on its last.
    /// The first / last subscriber of an event installs / removes the native listener or game hook that feeds it.
    /// </summary>
    internal static readonly SubscriberRouter<CoreEvent, EventSubscriber> Router = new(OnFirst, OnLast);

    private static HookListener? HookFor( CoreEvent kind ) => kind switch {
        CoreEvent.ClientProcessUsercmds => HookListener.ProcessUsercmds,
        CoreEvent.EntityTakeDamage => HookListener.TakeDamage,
        CoreEvent.EntityStartTouch => HookListener.StartTouch,
        CoreEvent.EntityTouch => HookListener.Touch,
        CoreEvent.EntityEndTouch => HookListener.EndTouch,
        CoreEvent.CanAcquire => HookListener.CanAcquire,
        CoreEvent.CanUseWeapon => HookListener.CanUse,
        CoreEvent.WeaponDrop => HookListener.WeaponDrop,
        CoreEvent.RunCommand => HookListener.RunCommand,
        CoreEvent.PostThink => HookListener.PostThink,
        CoreEvent.AcceptInput => HookListener.AcceptInput,
        CoreEvent.FireOutput => HookListener.FireOutput,
        _ => null,
    };

    private static unsafe void OnFirst( CoreEvent kind )
    {
        if (HookFor(kind) is { } hook)
        {
            GameHooksPublisher.AddHookListener(hook);
            return;
        }

        switch (kind)
        {
            case CoreEvent.ConsoleOutput:
                nativeListenerIds[(int)kind] = NativeConsoleOutput.AddConsoleListener((nint)(delegate* unmanaged< nint, void >)&OnConsoleOutput);
                break;
            case CoreEvent.ConVarValueChanged:
                nativeListenerIds[(int)kind] = NativeConvars.AddGlobalChangeListener((nint)(delegate* unmanaged< nint, int, nint, nint, void >)&OnConVarValueChanged);
                break;
            case CoreEvent.ConVarCreated:
                nativeListenerIds[(int)kind] = NativeConvars.AddConvarCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConVarCreated);
                break;
            case CoreEvent.ConCommandCreated:
                nativeListenerIds[(int)kind] = NativeConvars.AddConCommandCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConCommandCreated);
                break;
        }
    }

    private static void OnLast( CoreEvent kind )
    {
        if (HookFor(kind) is { } hook)
        {
            GameHooksPublisher.RemoveHookListener(hook);
            return;
        }

        var id = nativeListenerIds[(int)kind];
        switch (kind)
        {
            case CoreEvent.ConsoleOutput: NativeConsoleOutput.RemoveConsoleListener(id); break;
            case CoreEvent.ConVarValueChanged: NativeConvars.RemoveGlobalChangeListener(id); break;
            case CoreEvent.ConVarCreated: NativeConvars.RemoveConvarCreatedListener(id); break;
            case CoreEvent.ConCommandCreated: NativeConvars.RemoveConCommandCreatedListener(id); break;
        }
    }

    private static bool Listens( CoreEvent kind ) => Router.Get(kind).Length != 0;

    private static void Report( Exception e )
    {
        if (GlobalExceptionHandler.Handle(ref e)) AnsiConsole.WriteException(e);
    }

    public static void Register()
    {
        unsafe
        {
            NativeEvents.RegisterOnGameTickCallback((nint)(delegate* unmanaged< byte, byte, byte, void >)&OnTick);
            NativeEvents.RegisterOnPreworldUpdateCallback((nint)(delegate* unmanaged< byte, void >)&OnPreworldUpdate);
            NativeEvents.RegisterOnClientConnectCallback((nint)(delegate* unmanaged< int, ulong, byte >)&OnClientConnected);
            NativeEvents.RegisterOnClientDisconnectCallback((nint)(delegate* unmanaged< int, int, void >)&OnClientDisconnected);
            NativeEvents.RegisterOnClientKeyStateChangedCallback((nint)(delegate* unmanaged< int, GameButtons, byte, void >)&OnClientKeyStateChanged);
            NativeEvents.RegisterOnClientPutInServerCallback((nint)(delegate* unmanaged< int, int, void >)&OnClientPutInServer);
            NativeEvents.RegisterOnClientSteamAuthorizeCallback((nint)(delegate* unmanaged< int, void >)&OnClientSteamAuthorize);
            NativeEvents.RegisterOnClientSteamAuthorizeFailCallback((nint)(delegate* unmanaged< int, void >)&OnClientSteamAuthorizeFail);
            NativeEvents.RegisterOnEntityCreatedCallback((nint)(delegate* unmanaged< nint, void >)&OnEntityCreated);
            NativeEvents.RegisterOnEntityDeletedCallback((nint)(delegate* unmanaged< nint, void >)&OnEntityDeleted);
            NativeEvents.RegisterOnEntityParentChangedCallback((nint)(delegate* unmanaged< nint, nint, void >)&OnEntityParentChanged);
            NativeEvents.RegisterOnEntitySpawnedCallback((nint)(delegate* unmanaged< nint, void >)&OnEntitySpawned);
            NativeEvents.RegisterOnStartupServerCallback((nint)(delegate* unmanaged< void >)&OnStartupServer);
            _ = NativeConvars.AddQueryClientCvarCallback((nint)(delegate* unmanaged< int, nint, nint, void >)&ConVarQueryCallback);
            NativeCommands.SetCommandHandler((nint)(delegate* unmanaged< nint, int, nint, nint, nint, byte, void >)&OnCommandDispatch);
            NativeNetMessages.SetNetMessageServerHook((nint)(delegate* unmanaged< nint, int, nint, int >)&OnNetMessageServerDispatch);
            NativeNetMessages.SetNetMessageClientHook((nint)(delegate* unmanaged< int, int, nint, int >)&OnNetMessageClientDispatch);
            NativeNetMessages.SetNetMessageServerHookInternal((nint)(delegate* unmanaged< int, int, nint, int >)&OnNetMessageServerInternalDispatch);
            NetMessageDispatcher.Initialize();
            NativeGameEvents.SetListenerPreHandler((nint)(delegate* unmanaged< uint, nint, nint, int >)&OnGameEventPreDispatch);
            NativeGameEvents.SetListenerPostHandler((nint)(delegate* unmanaged< uint, nint, nint, int >)&OnGameEventPostDispatch);
        }
    }

    [UnmanagedCallersOnly]
    public static void ConVarQueryCallback( int playerId, nint name, nint value )
    {
        try
        {
            var convarName = StringAlloc.CreateCSharpString(name);
            var convarValue = StringAlloc.CreateCSharpString(value);

            ConVarService.ProcessConVarQueryCallback(playerId, convarName, convarValue);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnCommandDispatch( nint commandNamePtr, int playerId, nint argsPtr, nint originalCommandNamePtr, nint prefixPtr, byte silent )
    {
        try
        {
            var commandName = StringAlloc.CreateCSharpString(commandNamePtr);
            var argsString = StringAlloc.CreateCSharpString(argsPtr);
            var originalCommandName = StringAlloc.CreateCSharpString(originalCommandNamePtr);
            var prefix = StringAlloc.CreateCSharpString(prefixPtr);

            var args = argsString.Split('\x01');
            if (args.Length < 2) args = [.. args.Where(s => !string.IsNullOrWhiteSpace(s))];

            CommandDispatcher.DispatchCommand(commandName, playerId, args, originalCommandName, prefix, silent == 1);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static int OnClientCommandDispatch( int playerId, nint commandLinePtr )
    {
        try
        {
            var commandLine = StringAlloc.CreateCSharpString(commandLinePtr);
            return CommandDispatcher.DispatchClientCommand(playerId, commandLine);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnClientChatDispatch( int playerId, nint textPtr, byte teamonly )
    {
        try
        {
            var text = StringAlloc.CreateCSharpString(textPtr);
            return CommandDispatcher.DispatchClientChat(playerId, text, teamonly == 1);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnNetMessageServerDispatch( nint pPlayerMask, int msgId, nint pMessage )
    {
        try
        {
            return NetMessageDispatcher.DispatchServerMessage(pPlayerMask, msgId, pMessage);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnNetMessageClientDispatch( int playerId, int msgId, nint pMessage )
    {
        try
        {
            return NetMessageDispatcher.DispatchClientMessage(playerId, msgId, pMessage);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnNetMessageServerInternalDispatch( int playerId, int msgId, nint pMessage )
    {
        try
        {
            return NetMessageDispatcher.DispatchServerInternalMessage(playerId, msgId, pMessage);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnGameEventPreDispatch( uint hash, nint pEvent, nint pDontBroadcast )
    {
        try
        {
            return GameEventDispatcher.DispatchPre(hash, pEvent, pDontBroadcast);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnGameEventPostDispatch( uint hash, nint pEvent, nint pDontBroadcast )
    {
        try
        {
            return GameEventDispatcher.DispatchPost(hash, pEvent, pDontBroadcast);
        }
        catch (Exception e)
        {
            Report(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarCreated( nint convarNamePtr )
    {
        var subs = Router.Get(CoreEvent.ConVarCreated);
        if (subs.Length == 0) return;

        try
        {
            OnConVarCreated @event = new() { ConVarName = StringAlloc.CreateCSharpString(convarNamePtr) };
            foreach (var sub in subs) sub.InvokeOnConVarCreated(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConCommandCreated( nint commandNamePtr )
    {
        var subs = Router.Get(CoreEvent.ConCommandCreated);
        if (subs.Length == 0) return;

        try
        {
            OnConCommandCreated @event = new() { CommandName = StringAlloc.CreateCSharpString(commandNamePtr) };
            foreach (var sub in subs) sub.InvokeOnConCommandCreated(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarValueChanged( nint convarNamePtr, int playerid, nint newValuePtr, nint oldValuePtr )
    {
        var subs = Router.Get(CoreEvent.ConVarValueChanged);
        if (subs.Length == 0) return;

        try
        {
            OnConVarValueChanged @event = new() {
                PlayerId = playerid,
                ConVarName = StringAlloc.CreateCSharpString(convarNamePtr),
                NewValue = StringAlloc.CreateCSharpString(newValuePtr),
                OldValue = StringAlloc.CreateCSharpString(oldValuePtr),
            };
            foreach (var sub in subs) sub.InvokeOnConVarValueChanged(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnTick( byte simulating, byte first, byte last )
    {
        try
        {
            SchedulerManager.OnTick();

            foreach (var sub in Router.Get(CoreEvent.Tick)) sub.InvokeOnTick();
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPreworldUpdate( byte simulating )
    {
        try
        {
            SchedulerManager.OnWorldUpdate();

            foreach (var sub in Router.Get(CoreEvent.WorldUpdate)) sub.InvokeOnWorldUpdate();
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnClientConnected( int playerId, ulong unauthorizedSteamID )
    {
        try
        {
            PlayerManagerService.RegisterPlayerObject(playerId, unauthorizedSteamID);

            var subs = Router.Get(CoreEvent.ClientConnected);
            if (subs.Length == 0) return 1;

            OnClientConnectedEvent @event = new() { PlayerId = playerId };
            foreach (var sub in subs)
            {
                sub.InvokeOnClientConnected(ref @event);

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

            if (@event.Result == HookResult.CancelOriginal)
            {
                PlayerManagerService.UnregisterPlayerObject(playerId);
                return 0;
            }

            return 1;
        }
        catch (Exception e)
        {
            Report(e);
            return 1;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientDisconnected( int playerId, int reason )
    {
        try
        {
            var subs = Router.Get(CoreEvent.ClientDisconnected);
            if (subs.Length == 0) return;

            OnClientDisconnectedEvent @event = new() {
                PlayerId = playerId,
                Reason = (ENetworkDisconnectionReason)reason
            };
            foreach (var sub in subs) sub.InvokeOnClientDisconnected(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
        finally
        {
            PlayerManagerService.UnregisterPlayerObject(playerId);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientKeyStateChanged( int playerId, GameButtons key, byte pressed )
    {
        var subs = Router.Get(CoreEvent.ClientKeyStateChanged);
        if (subs.Length == 0) return;

        try
        {
            OnClientKeyStateChangedEvent @event = new() {
                PlayerId = playerId,
                Key = key.ToKeyKind(),
                Pressed = pressed != 0
            };
            foreach (var sub in subs) sub.InvokeOnClientKeyStateChanged(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientPutInServer( int playerId, int clientKind )
    {
        try
        {
            var subs = Router.Get(CoreEvent.ClientPutInServer);
            if (subs.Length == 0) return;

            OnClientPutInServerEvent @event = new() {
                PlayerId = playerId,
                Kind = (ClientKind)clientKind
            };
            foreach (var sub in subs) sub.InvokeOnClientPutInServer(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorize( int playerId )
    {
        var subs = Router.Get(CoreEvent.ClientSteamAuthorize);
        if (subs.Length == 0) return;

        try
        {
            OnClientSteamAuthorizeEvent @event = new() { PlayerId = playerId };
            foreach (var sub in subs) sub.InvokeOnClientSteamAuthorize(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorizeFail( int playerId )
    {
        var subs = Router.Get(CoreEvent.ClientSteamAuthorizeFail);
        if (subs.Length == 0) return;

        try
        {
            OnClientSteamAuthorizeFailEvent @event = new() { PlayerId = playerId };
            foreach (var sub in subs) sub.InvokeOnClientSteamAuthorizeFail(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityCreated( nint entityPtr )
    {
        try
        {
            var entity = EntityManager.OnEntityCreated(entityPtr);

            var subs = Router.Get(CoreEvent.EntityCreated);
            if (subs.Length == 0) return;

            OnEntityCreatedEvent @event = new() { Entity = entity };
            foreach (var sub in subs) sub.InvokeOnEntityCreated(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityDeleted( nint entityPtr )
    {
        try
        {
            var subs = Router.Get(CoreEvent.EntityDeleted);
            if (subs.Length == 0) return;

            var entity = EntityManager.GetEntityByAddress(entityPtr);
            OnEntityDeletedEvent @event = new() { Entity = entity! };
            foreach (var sub in subs) sub.InvokeOnEntityDeleted(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
        finally
        {
            EntityManager.OnEntityDeleted(entityPtr);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityParentChanged( nint entityPtr, nint newParentPtr )
    {
        var subs = Router.Get(CoreEvent.EntityParentChanged);
        if (subs.Length == 0) return;

        try
        {
            OnEntityParentChangedEvent @event = new() {
                Entity = EntityManager.GetEntityByAddress(entityPtr) ?? new CEntityInstanceImpl(entityPtr),
                NewParent = newParentPtr != 0 ? EntityManager.GetEntityByAddress(newParentPtr) ?? new CEntityInstanceImpl(newParentPtr) : null
            };
            foreach (var sub in subs) sub.InvokeOnEntityParentChanged(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntitySpawned( nint entityPtr )
    {
        var subs = Router.Get(CoreEvent.EntitySpawned);
        if (subs.Length == 0) return;

        try
        {
            OnEntitySpawnedEvent @event = new() { Entity = EntityManager.GetEntityByAddress(entityPtr) ?? new CEntityInstanceImpl(entityPtr) };
            foreach (var sub in subs) sub.InvokeOnEntitySpawned(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void OnMapLoad( string mapName )
    {
        try
        {
            HelpersService.RenewVDataCache();

            var subs = Router.Get(CoreEvent.MapLoad);
            if (subs.Length == 0) return;

            OnMapLoadEvent @event = new() { MapName = mapName };
            foreach (var sub in subs) sub.InvokeOnMapLoad(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void InvokeOnClientVoice( int playerId )
    {
        var subs = Router.Get(CoreEvent.ClientVoice);
        if (subs.Length == 0) return;

        try
        {
            OnClientVoiceEvent @event = new() { PlayerId = playerId };
            foreach (var sub in subs) sub.InvokeOnClientVoice(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void InvokeOnClientSettingsChanged( int playerId )
    {
        var subs = Router.Get(CoreEvent.ClientSettingsChanged);
        if (subs.Length == 0) return;

        try
        {
            OnClientSettingsChangedEvent @event = new() { PlayerId = playerId };
            foreach (var sub in subs) sub.InvokeOnClientSettingsChanged(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void OnMapUnload( string mapName )
    {
        EntitySystemService.cachedGameRulesProxy = null;

        var subs = Router.Get(CoreEvent.MapUnload);
        if (subs.Length == 0) return;

        try
        {
            OnMapUnloadEvent @event = new() { MapName = mapName };
            foreach (var sub in subs) sub.InvokeOnMapUnload(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToProcessUsercmds => Listens(CoreEvent.ClientProcessUsercmds);

    public static void OnClientProcessUsercmds( ref OnClientProcessUsercmdsEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.ClientProcessUsercmds)) sub.InvokeOnClientProcessUsercmds(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToTakeDamage => Listens(CoreEvent.EntityTakeDamage);

    public static void InvokeOnEntityTakeDamage( ref OnEntityTakeDamageEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.EntityTakeDamage))
            {
                sub.InvokeOnEntityTakeDamage(ref @event);

                if (@event.Result == HookResult.Handled || @event.Result == HookResult.Stop)
                {
                    return;
                }
            }
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void OnPrecacheResource( nint pResourceManifest )
    {
        var subs = Router.Get(CoreEvent.PrecacheResource);
        if (subs.Length == 0) return;

        try
        {
            OnPrecacheResourceEvent @event = new() { pResourceManifest = pResourceManifest };
            foreach (var sub in subs) sub.InvokeOnPrecacheResource(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnStartupServer()
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.StartupServer)) sub.InvokeOnStartupServer();
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToEntityStartTouch => Listens(CoreEvent.EntityStartTouch);

    public static void InvokeOnEntityStartTouch( OnEntityStartTouchEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.EntityStartTouch)) sub.InvokeOnEntityStartTouch(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToEntityTouch => Listens(CoreEvent.EntityTouch);

    public static void InvokeOnEntityTouch( OnEntityTouchEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.EntityTouch)) sub.InvokeOnEntityTouch(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToEntityEndTouch => Listens(CoreEvent.EntityEndTouch);

    public static void InvokeOnEntityEndTouch( OnEntityEndTouchEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.EntityEndTouch)) sub.InvokeOnEntityEndTouch(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void InvokeOnSteamAPIActivatedHook()
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.SteamAPIActivated)) sub.InvokeOnSteamAPIActivatedHook();
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToCanAcquire => Listens(CoreEvent.CanAcquire);

    public static void InvokeOnCanAcquireHook( OnItemServicesCanAcquireHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.CanAcquire))
            {
                sub.InvokeOnItemServicesCanAcquireHook(ref @event);
                if (@event.Intercepted) break;
            }
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToCanUseWeapon => Listens(CoreEvent.CanUseWeapon);

    public static void InvokeOnWeaponServicesCanUseHook( OnWeaponServicesCanUseHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.CanUseWeapon)) sub.InvokeOnWeaponServicesCanUseHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConsoleOutput( nint messagePtr )
    {
        var subs = Router.Get(CoreEvent.ConsoleOutput);
        if (subs.Length == 0) return;

        try
        {
            OnConsoleOutputEvent @event = new() { Message = StringAlloc.CreateCSharpString(messagePtr) };
            foreach (var sub in subs) sub.InvokeOnConsoleOutput(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void InvokeOnCommandExecuteHook( OnCommandExecuteHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.CommandExecuteHook)) sub.InvokeOnCommandExecuteHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToRunCommand => Listens(CoreEvent.RunCommand);

    public static void InvokeOnMovementServicesRunCommandHook( OnMovementServicesRunCommandHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.RunCommand)) sub.InvokeOnMovementServicesRunCommandHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToPostThink => Listens(CoreEvent.PostThink);

    public static void InvokeOnPlayerPawnPostThinkHook( OnPlayerPawnPostThinkHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.PostThink)) sub.InvokeOnPlayerPawnPostThinkHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToAcceptInput => Listens(CoreEvent.AcceptInput);

    public static void InvokeOnEntityIdentityAcceptInputHook( OnEntityIdentityAcceptInputHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.AcceptInput)) sub.InvokeOnEntityIdentityAcceptInputHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToWeaponDrop => Listens(CoreEvent.WeaponDrop);

    public static void InvokeOnWeaponServicesDropWeaponHook( OnWeaponServicesDropWeaponHook @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.WeaponDrop)) sub.InvokeOnWeaponServicesDropWeaponHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static bool ListensToFireOutput => Listens(CoreEvent.FireOutput);

    public static void InvokeEntityFireOutputHook( OnEntityFireOutputHookEvent @event )
    {
        try
        {
            foreach (var sub in Router.Get(CoreEvent.FireOutput)) sub.InvokeOnEntityFireOutputHook(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }

    public static void InvokeOnCustomHudClicked( int playerId, nint pMessage )
    {
        var subs = Router.Get(CoreEvent.CustomHudClicked);
        if (subs.Length == 0) return;

        try
        {
            var msg = new CCSUsrMsg_CustomHudClickedImpl(pMessage, false);
            OnCustomHudClickedEvent @event = new() { Message = msg, PlayerId = playerId };
            if (!@event.IsValid) return;

            foreach (var sub in subs) sub.InvokeOnCustomHudClicked(ref @event);
        }
        catch (Exception e)
        {
            Report(e);
        }
    }
}
