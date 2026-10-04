using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameEvents;

internal abstract class GameEventCallback : IEquatable<GameEventCallback>, IDisposable
{

    public Guid Guid { get; init; }

    public string EventName { get; init; } = "";

    public Type EventType { get; init; } = typeof(object);

    public bool IsPreHook { get; init; }

    public IContextedProfilerService Profiler { get; }

    public ILoggerFactory LoggerFactory { get; }

    public CoreContext Context { get; }

    protected GameEventCallback( ILoggerFactory loggerFactory, IContextedProfilerService profiler, CoreContext context )
    {
        LoggerFactory = loggerFactory;
        Profiler = profiler;
        Context = context;
    }

    internal virtual HookResult InvokeAsPre( uint hash, nint pEvent, nint pDontBroadcast ) => HookResult.Continue;
    internal virtual HookResult InvokeAsPost( uint hash, nint pEvent, nint pDontBroadcast ) => HookResult.Continue;

    public abstract void Dispose();

    public bool Equals( GameEventCallback? other )
    {
        return other is not null && Guid == other.Guid;
    }

    public override bool Equals( object? obj )
    {
        return ReferenceEquals(this, obj) || (obj is GameEventCallback other && Equals(other));
    }

    public override int GetHashCode()
    {
        return Guid.GetHashCode();
    }
}

internal class GameEventCallback<T> : GameEventCallback, IDisposable where T : IGameEvent<T>
{
    private static readonly uint s_hash = T.GetHash();
    private static readonly string s_eventName = T.GetName();

    private IGameEventService.GameEventHandler<T> _callback { get; init; }
    private ILogger<GameEventCallback<T>> _Logger { get; init; }

    public GameEventCallback( IGameEventService.GameEventHandler<T> callback, bool pre, ILoggerFactory loggerFactory, IContextedProfilerService profiler, CoreContext context ) : base(loggerFactory, profiler, context)
    {
        Guid = Guid.NewGuid();
        EventType = typeof(T);
        IsPreHook = pre;
        EventName = s_eventName;
        _callback = callback;
        _Logger = LoggerFactory.CreateLogger<GameEventCallback<T>>();
        NativeGameEvents.RegisterListener(s_eventName);
        GameEventService.RegisterCallback(this);
    }

    private HookResult Invoke( uint hash, nint pEvent, nint pDontBroadcast )
    {
        if (hash != s_hash) return HookResult.Continue;

        try
        {
            var eventObj = T.Create(pEvent);
            var result = _callback(eventObj);
            pDontBroadcast.Write(eventObj.DontBroadcast);
            eventObj.Dispose();
            return result;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            _Logger.LogError(e, "Error in event {EventName} callback from context {ContextName}", s_eventName, Context.Name);
            return HookResult.Continue;
        }
    }

    internal override HookResult InvokeAsPre( uint hash, nint pEvent, nint pDontBroadcast )
        => IsPreHook ? Invoke(hash, pEvent, pDontBroadcast) : HookResult.Continue;

    internal override HookResult InvokeAsPost( uint hash, nint pEvent, nint pDontBroadcast )
        => IsPreHook ? HookResult.Continue : Invoke(hash, pEvent, pDontBroadcast);

    public override void Dispose()
    {
        GameEventService.UnregisterCallback(this);
    }
}
