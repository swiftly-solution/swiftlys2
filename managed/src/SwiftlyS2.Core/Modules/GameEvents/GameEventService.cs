using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameEvents;

internal class GameEventService : IGameEventService, IDisposable
{
    private ILoggerFactory _LoggerFactory { get; init; }
    private CoreContext _Context { get; init; }
    private IContextedProfilerService _Profiler { get; init; }

    public GameEventService( ILoggerFactory loggerFactory, CoreContext context, IContextedProfilerService profiler )
    {
        _LoggerFactory = loggerFactory;
        _Context = context;
        _Profiler = profiler;
    }

    private readonly List<GameEventCallback> _callbacks = [];
    private readonly Lock _lock = new();

    public Guid HookPre<T>( IGameEventService.GameEventHandler<T> callback ) where T : IGameEvent<T> =>
        AddCallback(new GameEventCallback<T>(callback, true, _LoggerFactory, _Profiler, _Context));

    public Guid HookPost<T>( IGameEventService.GameEventHandler<T> callback ) where T : IGameEvent<T> =>
        AddCallback(new GameEventCallback<T>(callback, false, _LoggerFactory, _Profiler, _Context));

    public void Unhook( Guid guid ) => RemoveCallbacks(callback => callback.Guid == guid);

    public void UnhookPre<T>() where T : IGameEvent<T> =>
        RemoveCallbacks(callback => callback.IsPreHook && callback is GameEventCallback<T>);

    public void UnhookPost<T>() where T : IGameEvent<T> =>
        RemoveCallbacks(callback => !callback.IsPreHook && callback is GameEventCallback<T>);

    private Guid AddCallback( GameEventCallback callback )
    {
        lock (_lock)
        {
            _callbacks.Add(callback);
            GameEventDispatcher.Register(callback);
        }

        return callback.Guid;
    }

    private void RemoveCallbacks( Predicate<GameEventCallback> match )
    {
        lock (_lock)
        {
            foreach (var callback in _callbacks.FindAll(match))
            {
                GameEventDispatcher.Unregister(callback);
            }

            _ = _callbacks.RemoveAll(match);
        }
    }

    public void Fire<T>() where T : IGameEvent<T>
    {
        var handle = NativeGameEvents.CreateEvent(T.GetName());

        var playerIds = PlayerManagerService.PlayerObjects.Keys;
        foreach (var playerId in playerIds)
        {
            if (NativeGameEvents.IsPlayerListeningToEventName(playerId, T.GetName()))
            {
                NativeGameEvents.FireEventToClient(handle, playerId);
            }
        }

        NativeGameEvents.FreeEvent(handle);
    }

    public void Fire<T>( Action<T> configureEvent ) where T : IGameEvent<T>
    {
        var handle = NativeGameEvents.CreateEvent(T.GetName());
        var eventObj = T.Create(handle);
        configureEvent(eventObj);
        eventObj.Dispose();
        var playerIds = PlayerManagerService.PlayerObjects.Keys;
        foreach (var playerId in playerIds)
        {
            if (NativeGameEvents.IsPlayerListeningToEventName(playerId, T.GetName()))
            {
                NativeGameEvents.FireEventToClient(handle, playerId);
            }
        }

        NativeGameEvents.FreeEvent(handle);
    }

    public Task FireAsync<T>() where T : IGameEvent<T>
    {
        return SchedulerManager.QueueOrNow(Fire<T>);
    }

    public Task FireAsync<T>( Action<T> configureEvent ) where T : IGameEvent<T>
    {
        return SchedulerManager.QueueOrNow(() => Fire(configureEvent));
    }

    public void FireToPlayer<T>( int slot ) where T : IGameEvent<T>
    {
        var handle = NativeGameEvents.CreateEvent(T.GetName());
        NativeGameEvents.FireEventToClient(handle, slot);
        NativeGameEvents.FreeEvent(handle);
    }

    public void FireToPlayer<T>( int slot, Action<T> configureEvent ) where T : IGameEvent<T>
    {
        var handle = NativeGameEvents.CreateEvent(T.GetName());
        var eventObj = T.Create(handle);
        configureEvent(eventObj);
        eventObj.Dispose();
        NativeGameEvents.FireEventToClient(handle, slot);
        NativeGameEvents.FreeEvent(handle);
    }

    public Task FireToPlayerAsync<T>( int slot ) where T : IGameEvent<T>
    {
        return SchedulerManager.QueueOrNow(() => FireToPlayer<T>(slot));
    }

    public Task FireToPlayerAsync<T>( int slot, Action<T> configureEvent ) where T : IGameEvent<T>
    {
        return SchedulerManager.QueueOrNow(() => FireToPlayer(slot, configureEvent));
    }

    public void FireToServer<T>() where T : IGameEvent<T>
    {
        var handle = NativeGameEvents.CreateEvent(T.GetName());
        NativeGameEvents.FireEvent(handle, true);
    }

    public void FireToServer<T>( Action<T> configureEvent ) where T : IGameEvent<T>
    {
        var handle = NativeGameEvents.CreateEvent(T.GetName());
        var eventObj = T.Create(handle);
        configureEvent(eventObj);
        eventObj.Dispose();
        NativeGameEvents.FireEvent(handle, true);
    }

    public Task FireToServerAsync<T>() where T : IGameEvent<T>
    {
        return SchedulerManager.QueueOrNow(FireToServer<T>);
    }

    public Task FireToServerAsync<T>( Action<T> configureEvent ) where T : IGameEvent<T>
    {
        return SchedulerManager.QueueOrNow(() => FireToServer(configureEvent));
    }

    public bool IsListeningToEvent( int slot, string eventName )
    {
        return NativeGameEvents.IsPlayerListeningToEventName(slot, eventName);
    }

    public bool IsListeningToEvent<T>( int slot ) where T : IGameEvent<T>
    {
        return IsListeningToEvent(slot, T.GetName());
    }

    public void Dispose()
    {
        RemoveCallbacks(static _ => true);
    }
}
