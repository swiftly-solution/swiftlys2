using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Commands;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Core.ProtobufDefinitions;

namespace SwiftlyS2.Core.Events;

internal static class EventPublisher
{
    private static readonly List<EventSubscriber> subscribers = [];
    private static EventSubscriber[] subscriberSnapshot = [];
    private static readonly Lock subscribersLock = new();
    private static readonly Lock consoleOutputListenerLock = new();
    private static int consoleOutputSubscriberCount;
    private static ulong? consoleOutputListenerId;

    public static void Subscribe( EventSubscriber subscriber )
    {
        lock (subscribersLock)
        {
            subscribers.Add(subscriber);
            System.Threading.Volatile.Write(ref subscriberSnapshot, [.. subscribers]);
        }
    }

    public static void Unsubscribe( EventSubscriber subscriber )
    {
        lock (subscribersLock)
        {
            _ = subscribers.Remove(subscriber);
            System.Threading.Volatile.Write(ref subscriberSnapshot, [.. subscribers]);
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
            NativeEvents.RegisterOnPrecacheResourceCallback((nint)(delegate* unmanaged< nint, void >)&OnPrecacheResource);
            NativeEvents.RegisterOnStartupServerCallback((nint)(delegate* unmanaged< void >)&OnStartupServer);
            NativeEvents.RegisterOnClientVoiceCallback((nint)(delegate* unmanaged< int, void >)&OnClientVoice);
            _ = NativeConvars.AddConvarCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConVarCreated);
            _ = NativeConvars.AddConCommandCreatedListener((nint)(delegate* unmanaged< nint, void >)&OnConCommandCreated);
            _ = NativeConvars.AddGlobalChangeListener((nint)(delegate* unmanaged< nint, int, nint, nint, void >)&OnConVarValueChanged);
            NativeCommands.SetCommandHandler((nint)(delegate* unmanaged< nint, int, nint, nint, nint, byte, void >)&OnCommandDispatch);
            NativeCommands.SetClientCommandHandler((nint)(delegate* unmanaged< int, nint, int >)&OnClientCommandDispatch);
            NativeCommands.SetClientChatHandler((nint)(delegate* unmanaged< int, nint, byte, int >)&OnClientChatDispatch);
            NativeNetMessages.SetNetMessageServerHook((nint)(delegate* unmanaged< nint, int, nint, int >)&OnNetMessageServerDispatch);
            NativeNetMessages.SetNetMessageClientHook((nint)(delegate* unmanaged< int, int, nint, int >)&OnNetMessageClientDispatch);
            NativeNetMessages.SetNetMessageServerHookInternal((nint)(delegate* unmanaged< int, int, nint, int >)&OnNetMessageServerInternalDispatch);
            NativeGameEvents.SetListenerPreHandler((nint)(delegate* unmanaged< uint, nint, nint, int >)&OnGameEventPreDispatch);
            NativeGameEvents.SetListenerPostHandler((nint)(delegate* unmanaged< uint, nint, nint, int >)&OnGameEventPostDispatch);
        }
    }

    public static void AddConsoleOutputListener()
    {
        lock (consoleOutputListenerLock)
        {
            consoleOutputSubscriberCount++;
            if (consoleOutputSubscriberCount != 1) return;

            unsafe
            {
                consoleOutputListenerId = NativeConsoleOutput.AddConsoleListener(
                    (nint)(delegate* unmanaged< nint, void >)&OnConsoleOutput
                );
            }
        }
    }

    public static void RemoveConsoleOutputListener()
    {
        lock (consoleOutputListenerLock)
        {
            if (consoleOutputSubscriberCount == 0) return;

            consoleOutputSubscriberCount--;
            if (consoleOutputSubscriberCount != 0 || consoleOutputListenerId == null) return;

            NativeConsoleOutput.RemoveConsoleListener(consoleOutputListenerId.Value);
            consoleOutputListenerId = null;
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

            CommandService.DispatchCommand(commandName, playerId, args, originalCommandName, prefix, silent == 1);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return;
            AnsiConsole.WriteException(e);
        }
    }

    [UnmanagedCallersOnly]
    public static int OnClientCommandDispatch( int playerId, nint commandLinePtr )
    {
        try
        {
            var commandLine = StringAlloc.CreateCSharpString(commandLinePtr);
            return CommandService.DispatchClientCommand(playerId, commandLine);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnClientChatDispatch( int playerId, nint textPtr, byte teamonly )
    {
        try
        {
            var text = StringAlloc.CreateCSharpString(textPtr);
            return CommandService.DispatchClientChat(playerId, text, teamonly == 1);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnNetMessageServerDispatch( nint pPlayerMask, int msgId, nint pMessage )
    {
        try
        {
            return NetMessageService.DispatchServerMessage(pPlayerMask, msgId, pMessage);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnNetMessageClientDispatch( int playerId, int msgId, nint pMessage )
    {
        try
        {
            return NetMessageService.DispatchClientMessage(playerId, msgId, pMessage);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnNetMessageServerInternalDispatch( int playerId, int msgId, nint pMessage )
    {
        try
        {
            return NetMessageService.DispatchServerInternalMessage(playerId, msgId, pMessage);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnGameEventPreDispatch( uint hash, nint pEvent, nint pDontBroadcast )
    {
        try
        {
            return GameEventService.DispatchPreEvent(hash, pEvent, pDontBroadcast);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    [UnmanagedCallersOnly]
    public static int OnGameEventPostDispatch( uint hash, nint pEvent, nint pDontBroadcast )
    {
        try
        {
            return GameEventService.DispatchPostEvent(hash, pEvent, pDontBroadcast);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return 0;
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static bool ListensToConVarCreated {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToConVarCreated) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarCreated( nint convarNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToConVarCreated) return;

        try
        {
            OnConVarCreated @event = new() { ConVarName = StringAlloc.CreateCSharpString(convarNamePtr) };
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

    public static bool ListensToConCommandCreated {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToConCommandCreated) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConCommandCreated( nint commandNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToConCommandCreated) return;

        try
        {
            OnConCommandCreated @event = new() { CommandName = StringAlloc.CreateCSharpString(commandNamePtr) };
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

    public static bool ListensToConVarValueChanged {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToConVarValueChanged) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConVarValueChanged( nint convarNamePtr, int playerid, nint newValuePtr, nint oldValuePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToConVarValueChanged) return;

        try
        {
            OnConVarValueChanged @event = new() {
                PlayerId = playerid,
                ConVarName = StringAlloc.CreateCSharpString(convarNamePtr),
                NewValue = StringAlloc.CreateCSharpString(newValuePtr),
                OldValue = StringAlloc.CreateCSharpString(oldValuePtr),
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

    public static bool ListensToTick {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToTick) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnTick( byte simulating, byte first, byte last )
    {
        try
        {
            SchedulerManager.OnTick();

            if (subscribers.Count == 0)
            {
                return;
            }

            if (!ListensToTick) return;

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

    public static bool ListensToWorldUpdate {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToWorldUpdate) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPreworldUpdate( byte simulating )
    {
        try
        {
            SchedulerManager.OnWorldUpdate();

            if (subscribers.Count == 0)
            {
                return;
            }

            if (!ListensToWorldUpdate) return;

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

    public static bool ListensToClientConnected {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientConnected) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static byte OnClientConnected( int playerId )
    {
        try
        {
            PlayerManagerService.RegisterPlayerObject(playerId);
            if (subscribers.Count == 0)
            {
                return 1;
            }

            if (!ListensToClientConnected) return 1;

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

            if (@event.Result == HookResult.CancelOriginal) return 0;

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

    public static bool ListensToClientDisconnected {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientDisconnected) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientDisconnected( int playerId, int reason )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToClientDisconnected) return;

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

    public static bool ListensToClientKeyStateChanged {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientKeyStateChanged) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientKeyStateChanged( int playerId, GameButtons key, byte pressed )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToClientKeyStateChanged) return;

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

    public static bool ListensToClientPutInServer {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientPutInServer) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientPutInServer( int playerId, int clientKind )
    {
        try
        {
            if (subscribers.Count == 0)
            {
                return;
            }

            if (clientKind == (int)ClientKind.Bot) PlayerManagerService.RegisterPlayerObject(playerId);

            if (!ListensToClientPutInServer) return;

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

    public static bool ListensToClientSteamAuthorize {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientSteamAuthorize) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorize( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToClientSteamAuthorize) return;

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

    public static bool ListensToClientSteamAuthorizeFail {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientSteamAuthorizeFail) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientSteamAuthorizeFail( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToClientSteamAuthorizeFail) return;

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

    public static bool ListensToEntityCreated {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntityCreated) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityCreated( nint entityPtr )
    {
        try
        {
            var entity = EntityManager.OnEntityCreated(entityPtr);
            if (subscribers.Count == 0)
            {
                return;
            }

            if (!ListensToEntityCreated) return;

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

    public static bool ListensToEntityDeleted {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntityDeleted) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityDeleted( nint entityPtr )
    {
        try
        {
            if (subscribers.Count == 0 || !ListensToEntityDeleted)
            {
                return;
            }

            var entity = EntityManager.GetEntityByAddress(entityPtr);
            OnEntityDeletedEvent @event = new() { Entity = entity! };
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityDeleted(ref @event);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return;
            AnsiConsole.WriteException(e);
        }
        finally
        {
            EntityManager.OnEntityDeleted(entityPtr);
        }
    }

    public static bool ListensToEntityParentChanged {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntityParentChanged) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntityParentChanged( nint entityPtr, nint newParentPtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToEntityParentChanged) return;

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

    public static bool ListensToEntitySpawned {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntitySpawned) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnEntitySpawned( nint entityPtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToEntitySpawned) return;

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

    public static bool ListensToMapLoad {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToMapLoad) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnMapLoad( nint mapNamePtr )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        HelpersService.RenewVDataCache();

        if (!ListensToMapLoad) return;

        try
        {
            OnMapLoadEvent @event = new() { MapName = StringAlloc.CreateCSharpString(mapNamePtr) };
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

    public static bool ListensToClientVoice {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToClientVoice) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnClientVoice( int playerId )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToClientVoice) return;

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

    public static bool ListensToMapUnload {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToMapUnload) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnMapUnload( nint mapNamePtr )
    {
        EntitySystemService.cachedGameRulesProxy = null;
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToMapUnload) return;

        try
        {
            OnMapUnloadEvent @event = new() { MapName = StringAlloc.CreateCSharpString(mapNamePtr) };
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

    public static bool ListensToProcessUsercmds {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToProcessUsercmds) return true;
            }
            return false;
        }
    }

    public static void OnClientProcessUsercmds( ref OnClientProcessUsercmdsEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
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

    public static bool ListensToTakeDamage {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToTakeDamage) return true;
            }
            return false;
        }
    }

    public static void InvokeOnEntityTakeDamage( ref OnEntityTakeDamageEvent @event )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnEntityTakeDamage(ref @event);

                if (@event.Result == HookResult.Handled || @event.Result == HookResult.Stop)
                {
                    return;
                }
            }
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e)) AnsiConsole.WriteException(e);
        }
    }

    public static bool ListensToPrecacheResource {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToPrecacheResource) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnPrecacheResource( nint pResourceManifest )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToPrecacheResource) return;

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

    public static bool ListensToStartupServer {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToStartupServer) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnStartupServer()
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        if (!ListensToStartupServer) return;

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

    public static bool ListensToEntityStartTouch {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntityStartTouch) return true;
            }
            return false;
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

    public static bool ListensToEntityTouch {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntityTouch) return true;
            }
            return false;
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

    public static bool ListensToEntityEndTouch {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToEntityEndTouch) return true;
            }
            return false;
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

    public static bool ListensToCanAcquire {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToCanAcquire) return true;
            }
            return false;
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

    public static bool ListensToCanUseWeapon {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToCanUseWeapon) return true;
            }
            return false;
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

    public static bool ListensToConsoleOutput {
        get {
            var currentSubscribers = System.Threading.Volatile.Read(ref subscriberSnapshot);
            for (var i = 0; i < currentSubscribers.Length; i++)
            {
                if (currentSubscribers[i].ListensToConsoleOutput) return true;
            }
            return false;
        }
    }

    [UnmanagedCallersOnly]
    public static void OnConsoleOutput( nint messagePtr )
    {
        try
        {
            var currentSubscribers = System.Threading.Volatile.Read(ref subscriberSnapshot);
            var isTracking = CommandTrackerManager.IsTracking;
            if (!isTracking && currentSubscribers.Length == 0)
            {
                return;
            }

            var message = string.Empty;
            var setMessage = false;
            if (isTracking)
            {
                message = StringAlloc.CreateCSharpString(messagePtr);
                setMessage = true;
                CommandTrackerManager.ProcessOutput(message);
            }

            var hasConsoleOutputListener = false;
            for (var i = 0; i < currentSubscribers.Length; i++)
            {
                if (!currentSubscribers[i].ListensToConsoleOutput) continue;
                hasConsoleOutputListener = true;
                break;
            }
            if (!hasConsoleOutputListener) return;

            OnConsoleOutputEvent @event = new() { Message = setMessage ? message : StringAlloc.CreateCSharpString(messagePtr) };
            for (var i = 0; i < currentSubscribers.Length; i++)
            {
                currentSubscribers[i].InvokeOnConsoleOutput(ref @event);
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
        CommandTrackerManager.ProcessCommand(@event);

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

    public static bool ListensToRunCommand {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToRunCommand) return true;
            }
            return false;
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

    public static bool ListensToPostThink {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToPostThink) return true;
            }
            return false;
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

    public static bool ListensToAcceptInput {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToAcceptInput) return true;
            }
            return false;
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

    public static bool ListensToWeaponDrop {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToWeaponDrop) return true;
            }
            return false;
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

    public static bool ListensToFireOutput {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToFireOutput) return true;
            }
            return false;
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

    public static bool ListensToCustomHudClicked {
        get {
            for (var i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].ListensToCustomHudClicked) return true;
            }
            return false;
        }
    }

    public static void InvokeOnCustomHudClicked( int playerId, nint pMessage )
    {
        if (subscribers.Count == 0)
        {
            return;
        }

        try
        {
            var msg = new CCSUsrMsg_CustomHudClickedImpl(pMessage, false);
            OnCustomHudClickedEvent @event = new() { Message = msg, PlayerId = playerId };
            if (!@event.IsValid)
            {
                return;
            }
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnCustomHudClicked(ref @event);
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
