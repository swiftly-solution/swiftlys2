using System.Reflection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;
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

  private readonly List<GameEventCallback> _callbacks = new();
  private Lock _lock = new();

  public Guid HookPre<T>( IGameEventService.GameEventHandler<T> callback ) where T : IGameEvent<T>
  {
    GameEventCallback<T> cb = new(callback, true, _LoggerFactory, _Profiler, _Context);
    lock (_lock)
    {
      _callbacks.Add(cb);
    }
    return cb.Guid;
  }

  public Guid HookPost<T>( IGameEventService.GameEventHandler<T> callback ) where T : IGameEvent<T>
  {
    GameEventCallback<T> cb = new(callback, false, _LoggerFactory, _Profiler, _Context);
    lock (_lock)
    {
      _callbacks.Add(cb);
    }
    return cb.Guid;
  }

  public void Unhook( Guid guid )
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
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


  public void UnhookPre<T>() where T : IGameEvent<T>
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback.IsPreHook && callback is GameEventCallback<T>)
        {
          callback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public void UnhookPost<T>() where T : IGameEvent<T>
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (!callback.IsPreHook && callback is GameEventCallback<T>)
        {
          callback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public void Fire<T>() where T : IGameEvent<T>
  {
    var handle = NativeGameEvents.CreateEvent(T.GetName());
    for (int i = 0; i < NativePlayerManager.GetPlayerCap(); i++)
    {
      if (NativeGameEvents.IsPlayerListeningToEventName(i, T.GetName()) && NativePlayerManager.IsPlayerOnline(i))
      {
        NativeGameEvents.FireEventToClient(handle, i);
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
    for (int i = 0; i < NativePlayerManager.GetPlayerCap(); i++)
    {
      if (NativeGameEvents.IsPlayerListeningToEventName(i, T.GetName()) && NativePlayerManager.IsPlayerOnline(i))
      {
        NativeGameEvents.FireEventToClient(handle, i);
      }
    }
    NativeGameEvents.FreeEvent(handle);
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

  public void Dispose()
  {
    lock (_lock)
    {
      foreach (var callback in _callbacks)
      {
        callback.Dispose();
      }
      _callbacks.Clear();
    }
  }
}