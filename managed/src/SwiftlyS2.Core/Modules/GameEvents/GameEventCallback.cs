using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.GameEvents;

internal abstract class GameEventCallback : IEquatable<GameEventCallback>
{
    public Guid Guid { get; } = Guid.NewGuid();

    public abstract uint EventHash { get; }

    public abstract string EventName { get; }

    public bool IsPreHook { get; }

    public IContextedProfilerService Profiler { get; }

    public ILoggerFactory LoggerFactory { get; }

    public CoreContext Context { get; }

    protected GameEventCallback( bool isPreHook, ILoggerFactory loggerFactory, IContextedProfilerService profiler, CoreContext context )
    {
        IsPreHook = isPreHook;
        LoggerFactory = loggerFactory;
        Profiler = profiler;
        Context = context;
    }

    internal abstract HookResult Invoke( nint pEvent, nint pDontBroadcast );

    public bool Equals( GameEventCallback? other ) => other is not null && Guid == other.Guid;

    public override bool Equals( object? obj ) => ReferenceEquals(this, obj) || (obj is GameEventCallback other && Equals(other));

    public override int GetHashCode() => Guid.GetHashCode();
}

internal sealed class GameEventCallback<T> : GameEventCallback where T : IGameEvent<T>
{
    private static readonly uint hash = T.GetHash();
    private static readonly string eventName = T.GetName();

    private readonly IGameEventService.GameEventHandler<T> callback;
    private readonly ILogger<GameEventCallback<T>> logger;

    public GameEventCallback( IGameEventService.GameEventHandler<T> callback, bool isPreHook, ILoggerFactory loggerFactory, IContextedProfilerService profiler, CoreContext context )
        : base(isPreHook, loggerFactory, profiler, context)
    {
        this.callback = callback;
        logger = loggerFactory.CreateLogger<GameEventCallback<T>>();
    }

    public override uint EventHash => hash;

    public override string EventName => eventName;

    internal override HookResult Invoke( nint pEvent, nint pDontBroadcast )
    {
        try
        {
            var eventObj = T.Create(pEvent);
            var result = callback(eventObj);
            pDontBroadcast.Write(eventObj.DontBroadcast);
            eventObj.Dispose();
            return result;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            logger.LogError(e, "Error in event {EventName} callback from context {ContextName}", eventName, Context.Name);
            return HookResult.Continue;
        }
    }
}
