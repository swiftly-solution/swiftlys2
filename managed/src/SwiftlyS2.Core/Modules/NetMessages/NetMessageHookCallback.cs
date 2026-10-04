using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.NetMessages;

internal abstract class NetMessageHookCallback : IDisposable
{

    public Guid Guid { get; init; }

    public IContextedProfilerService Profiler { get; }

    public ILoggerFactory LoggerFactory { get; }

    protected NetMessageHookCallback( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        LoggerFactory = loggerFactory;
        Profiler = profiler;
    }

    internal virtual HookResult InvokeAsClient( int playerId, int msgId, nint pMessage ) => HookResult.Continue;
    internal virtual HookResult InvokeAsServer( nint pPlayerMask, int msgId, nint pMessage ) => HookResult.Continue;
    internal virtual HookResult InvokeAsServerInternal( int playerId, int msgId, nint pMessage ) => HookResult.Continue;

    public abstract void Dispose();

}

internal class NetMessageClientHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private static readonly string s_typeName = typeof(T).Name;

    private INetMessageService.ClientNetMessageHandler<T> _callback;
    private ILogger<NetMessageClientHookCallback<T>> _logger;


    public NetMessageClientHookCallback( INetMessageService.ClientNetMessageHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageClientHookCallback<T>>();
        _callback = callback;
        NetMessageService.RegisterCallback(this);
    }

    internal override HookResult InvokeAsClient( int playerId, int msgId, nint pMessage )
    {
        if (msgId != T.MessageId) return HookResult.Continue;

        try
        {
            var msg = T.Wrap(pMessage, false);
            return _callback(msg, playerId);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            _logger.LogError(e, "Error in net message client hook callback for {MessageType}", s_typeName);
            return HookResult.Continue;
        }
    }

    public override void Dispose()
    {
        NetMessageService.UnregisterCallback(this);
    }

}

internal class NetMessageServerHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private static readonly string s_typeName = typeof(T).Name;

    private INetMessageService.ServerNetMessageHandler<T> _callback;
    private ILogger<NetMessageServerHookCallback<T>> _logger;

    public NetMessageServerHookCallback( INetMessageService.ServerNetMessageHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageServerHookCallback<T>>();
        _callback = callback;
        NetMessageService.RegisterCallback(this);
    }

    internal override HookResult InvokeAsServer( nint pPlayerMask, int msgId, nint pMessage )
    {
        if (msgId != T.MessageId) return HookResult.Continue;

        try
        {
            var msg = T.Wrap(pMessage, false);
            var mask = pPlayerMask.Read<ulong>();
            msg.Recipients.RecipientsMask = mask;
            var result = _callback(msg);
            pPlayerMask.Write(msg.Recipients.ToMask());
            return result;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            _logger.LogError(e, "Error in net message server hook callback for {MessageType}", s_typeName);
            return HookResult.Continue;
        }
    }

    public override void Dispose()
    {
        NetMessageService.UnregisterCallback(this);
    }

}

internal class NetMessageServerInternalHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private static readonly string s_typeName = typeof(T).Name;

    private INetMessageService.ServerNetMessageInternalHandler<T> _callback;
    private ILogger<NetMessageServerInternalHookCallback<T>> _logger;


    public NetMessageServerInternalHookCallback( INetMessageService.ServerNetMessageInternalHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageServerInternalHookCallback<T>>();
        _callback = callback;
        NetMessageService.RegisterCallback(this);
    }

    internal override HookResult InvokeAsServerInternal( int playerId, int msgId, nint pMessage )
    {
        if (msgId != T.MessageId) return HookResult.Continue;

        try
        {
            var msg = T.Wrap(pMessage, false);
            return _callback(msg, playerId);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
            _logger.LogError(e, "Error in net message server internal hook callback for {MessageType}", s_typeName);
            return HookResult.Continue;
        }
    }

    public override void Dispose()
    {
        NetMessageService.UnregisterCallback(this);
    }

}
