using System.Runtime.InteropServices;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.Extensions;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameEvents;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate HookResult UnmanagedEventCallback( uint hash, nint pEvent, nint pDontBroadcast );

internal abstract class GameEventCallback : IEquatable<GameEventCallback>, IDisposable
{

  public Guid Guid { get; init; }

  public string EventName { get; init; } = "";

  public Type EventType { get; init; } = typeof(object);

  public bool IsPreHook { get; init; }

  public nint UnmanagedWrapperPtr { get; init; }

  public ulong ListenerId { get; init; }

  public IContextedProfilerService Profiler { get; }

  public ILoggerFactory LoggerFactory { get; }

  public CoreContext Context { get; }

  protected GameEventCallback( ILoggerFactory loggerFactory, IContextedProfilerService profiler, CoreContext context )
  {
    LoggerFactory = loggerFactory;
    Profiler = profiler;
    Context = context;
  }

  public void Dispose()
  {
    if (IsPreHook)
    {
      NativeGameEvents.RemoveListenerPreCallback(ListenerId);
    }
    else
    {
      NativeGameEvents.RemoveListenerPostCallback(ListenerId);
    }
  }

  public bool Equals( GameEventCallback? other )
  {
    if (other is null) return false;
    return Guid == other.Guid;
  }

  public override bool Equals( object? obj )
  {
    if (ReferenceEquals(this, obj)) return true;
    return obj is GameEventCallback other && Equals(other);
  }

  public override int GetHashCode()
  {
    return Guid.GetHashCode();
  }
}

internal class GameEventCallback<T> : GameEventCallback, IDisposable where T : IGameEvent<T>
{
  private IGameEventService.GameEventHandler<T> _callback { get; init; }
  private ILogger<GameEventCallback<T>> _Logger { get; init; }
  private UnmanagedEventCallback _unmanagedCallback;

  public GameEventCallback( IGameEventService.GameEventHandler<T> callback, bool pre, ILoggerFactory loggerFactory, IContextedProfilerService profiler, CoreContext context ) : base(loggerFactory, profiler, context)
  {
    Guid = Guid.NewGuid();
    EventType = typeof(T);
    IsPreHook = pre;
    EventName = T.GetName();
    _callback = callback;
    _Logger = LoggerFactory.CreateLogger<GameEventCallback<T>>();

    _unmanagedCallback = ( hash, pEvent, pDontBroadcast ) =>
    {
      try
      {
        if (hash != T.GetHash()) return HookResult.Continue;
        var category = "GameEventCallback::" + EventName;
        Profiler.StartRecording(category);
        var eventObj = T.Create(pEvent);
        var result = _callback(eventObj);
        pDontBroadcast.Write(eventObj.DontBroadcast);
        eventObj.Dispose();
        Profiler.StopRecording(category);
        return result;
      }
      catch (Exception e)
      {
        if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
        _Logger.LogError(e, "Error in event {EventName} callback from context {ContextName}", EventName, Context.Name);
        return HookResult.Continue;
      }
    };
    UnmanagedWrapperPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
    NativeGameEvents.RegisterListener(EventName);
    ListenerId = IsPreHook ? NativeGameEvents.AddListenerPreCallback(UnmanagedWrapperPtr) : NativeGameEvents.AddListenerPostCallback(UnmanagedWrapperPtr);
  }
}
