using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.NetMessages;

internal abstract class NetMessageHookCallback : IEquatable<NetMessageHookCallback>
{

    public Guid Guid { get; } = Guid.NewGuid();

    public abstract NetMessageHookType HookType { get; }

    public abstract int MessageId { get; }

    public IContextedProfilerService Profiler { get; }

    public ILoggerFactory LoggerFactory { get; }

    protected NetMessageHookCallback( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        LoggerFactory = loggerFactory;
        Profiler = profiler;
    }

    internal virtual HookResult InvokeAsClient( int playerId, nint pMessage ) => HookResult.Continue;
    internal virtual HookResult InvokeAsServer( nint pPlayerMask, nint pMessage ) => HookResult.Continue;
    internal virtual HookResult InvokeAsServerInternal( int playerId, nint pMessage ) => HookResult.Continue;

    public bool Equals( NetMessageHookCallback? other ) => other is not null && Guid == other.Guid;

    public override bool Equals( object? obj ) => ReferenceEquals(this, obj) || (obj is NetMessageHookCallback other && Equals(other));

    public override int GetHashCode() => Guid.GetHashCode();

}

internal sealed class NetMessageClientHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private static readonly string typeName = typeof(T).Name;

    private readonly INetMessageService.ClientNetMessageHandler<T> callback;
    private readonly ILogger<NetMessageClientHookCallback<T>> logger;

    public NetMessageClientHookCallback( INetMessageService.ClientNetMessageHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        this.callback = callback;
        logger = loggerFactory.CreateLogger<NetMessageClientHookCallback<T>>();
    }

    public override NetMessageHookType HookType => NetMessageHookType.Client;

    public override int MessageId => T.MessageId;

    internal override HookResult InvokeAsClient( int playerId, nint pMessage )
    {
        try
        {
            var msg = T.Wrap(pMessage, false);
            return callback(msg, playerId);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            logger.LogError(e, "Error in net message client hook callback for {MessageType}", typeName);
            return HookResult.Continue;
        }
    }

}

internal sealed class NetMessageServerHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private static readonly string typeName = typeof(T).Name;

    private readonly INetMessageService.ServerNetMessageHandler<T> callback;
    private readonly ILogger<NetMessageServerHookCallback<T>> logger;

    public NetMessageServerHookCallback( INetMessageService.ServerNetMessageHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        this.callback = callback;
        logger = loggerFactory.CreateLogger<NetMessageServerHookCallback<T>>();
    }

    public override NetMessageHookType HookType => NetMessageHookType.Server;

    public override int MessageId => T.MessageId;

    internal override HookResult InvokeAsServer( nint pPlayerMask, nint pMessage )
    {
        try
        {
            var msg = T.Wrap(pMessage, false);
            var mask = pPlayerMask.Read<ulong>();
            msg.Recipients.RecipientsMask = mask;
            var result = callback(msg);
            pPlayerMask.Write(msg.Recipients.ToMask());
            return result;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            logger.LogError(e, "Error in net message server hook callback for {MessageType}", typeName);
            return HookResult.Continue;
        }
    }

}

internal sealed class NetMessageServerInternalHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private static readonly string typeName = typeof(T).Name;

    private readonly INetMessageService.ServerNetMessageInternalHandler<T> callback;
    private readonly ILogger<NetMessageServerInternalHookCallback<T>> logger;

    public NetMessageServerInternalHookCallback( INetMessageService.ServerNetMessageInternalHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        this.callback = callback;
        logger = loggerFactory.CreateLogger<NetMessageServerInternalHookCallback<T>>();
    }

    public override NetMessageHookType HookType => NetMessageHookType.ServerInternal;

    public override int MessageId => T.MessageId;

    internal override HookResult InvokeAsServerInternal( int playerId, nint pMessage )
    {
        try
        {
            var msg = T.Wrap(pMessage, false);
            return callback(msg, playerId);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            logger.LogError(e, "Error in net message server internal hook callback for {MessageType}", typeName);
            return HookResult.Continue;
        }
    }

}
