using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.NetMessages;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate HookResult NetMessageClientHookCallbackDelegate( int playerId, int msgId, nint pMessage );


[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate HookResult NetMessageServerHookCallbackDelegate( nint pPlayerMask, int msgId, nint pMessage );

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

    public abstract void Dispose();

}

internal class NetMessageClientHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private INetMessageService.ClientNetMessageHandler<T> _callback;
    private NetMessageClientHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeListenerId;
    private ILogger<NetMessageClientHookCallback<T>> _logger;


    public NetMessageClientHookCallback( INetMessageService.ClientNetMessageHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageClientHookCallback<T>>();

        _callback = callback;

        _unmanagedCallback = ( playerId, msgId, pMessage ) =>
        {
            try
            {
                if (msgId != T.MessageId) return HookResult.Continue;
                var category = "NetMessageClientHookCallback::" + typeof(T).Name;
                Profiler.StartRecording(category);
                var msg = T.Wrap(pMessage, false);
                var result = _callback(msg, playerId);
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
                _logger.LogError(e, "Error in net message client hook callback for {MessageType}", typeof(T).Name);
                return HookResult.Continue;
            }
        };
        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeListenerId = NativeNetMessages.AddNetMessageClientHook(_unmanagedCallbackPtr);

    }

    public override void Dispose()
    {
        NativeNetMessages.RemoveNetMessageClientHook(_nativeListenerId);
    }

}

internal class NetMessageServerHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private INetMessageService.ServerNetMessageHandler<T> _callback;
    private NetMessageServerHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeListenerId;
    private ILogger<NetMessageServerHookCallback<T>> _logger;

    public NetMessageServerHookCallback( INetMessageService.ServerNetMessageHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageServerHookCallback<T>>();

        _callback = callback;

        _unmanagedCallback = ( pPlayerMask, msgId, pMessage ) =>
        {
            try
            {
                if (msgId != T.MessageId) return HookResult.Continue;
                var category = "NetMessageServerHookCallback::" + typeof(T).Name;
                Profiler.StartRecording(category);
                var msg = T.Wrap(pMessage, false);
                var mask = pPlayerMask.Read<ulong>();
                msg.Recipients.RecipientsMask = mask;
                var result = _callback(msg);
                pPlayerMask.Write(msg.Recipients.ToMask());
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
                _logger.LogError(e, "Error in net message server hook callback for {MessageType}", typeof(T).Name);
                return HookResult.Continue;
            }
        };
        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeListenerId = NativeNetMessages.AddNetMessageServerHook(_unmanagedCallbackPtr);

    }

    public override void Dispose()
    {
        NativeNetMessages.RemoveNetMessageServerHook(_nativeListenerId);
    }

}

internal class NetMessageServerInternalHookCallback<T> : NetMessageHookCallback where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
{

    private INetMessageService.ServerNetMessageInternalHandler<T> _callback;
    private NetMessageClientHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeListenerId;
    private ILogger<NetMessageServerInternalHookCallback<T>> _logger;


    public NetMessageServerInternalHookCallback( INetMessageService.ServerNetMessageInternalHandler<T> callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageServerInternalHookCallback<T>>();

        _callback = callback;

        _unmanagedCallback = ( playerId, msgId, pMessage ) =>
        {
            try
            {
                if (msgId != T.MessageId) return HookResult.Continue;
                var category = "NetMessageServerInternalHookCallback::" + typeof(T).Name;
                Profiler.StartRecording(category);
                var msg = T.Wrap(pMessage, false);
                var result = _callback(msg, playerId);
                Profiler.StopRecording(category);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
                _logger.LogError(e, "Error in net message server internal hook callback for {MessageType}", typeof(T).Name);
                return HookResult.Continue;
            }
        };
        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeListenerId = NativeNetMessages.AddNetMessageServerHookInternal(_unmanagedCallbackPtr);

    }

    public override void Dispose()
    {
        NativeNetMessages.RemoveNetMessageServerHookInternal(_nativeListenerId);
    }

}

internal sealed class UntypedNetMessage : IUntypedNetMessage
{
    private readonly IProtobufAccessor _accessor;
    private readonly int _msgId;

    public int MessageId => _msgId;

    public string MessageName => NativeNetMessages.GetMessageNameById(_msgId);

    public IProtobufAccessor Accessor => _accessor;

    public string GetDebugString() => NativeNetMessages.DebugString(_accessor.Address);

    internal UntypedNetMessage(nint protoPtr, int msgId)
    {
        _accessor = new ProtobufAccessor(protoPtr);
        _msgId = msgId;
    }
}

// ── Raw (untyped) hooks – catch ALL messages ──────────────────────

internal class NetMessageClientRawHookCallback : NetMessageHookCallback
{

    private INetMessageService.RawClientNetMessageHandler _callback;
    private NetMessageClientHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeListenerId;
    private ILogger<NetMessageClientRawHookCallback> _logger;
    private static readonly int CNetMsgOffset = NativeNetMessages.GetCNetMessageSize();

    public NetMessageClientRawHookCallback( INetMessageService.RawClientNetMessageHandler callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageClientRawHookCallback>();

        _callback = callback;

        _unmanagedCallback = ( playerId, msgId, pMessage ) =>
        {
            try
            {
                var msg = new UntypedNetMessage(pMessage + CNetMsgOffset, msgId);
                var result = _callback(playerId, msg);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
                _logger.LogError(e, "Error in raw client net message hook callback (msgId={MsgId})", msgId);
                return HookResult.Continue;
            }
        };
        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeListenerId = NativeNetMessages.AddNetMessageClientHook(_unmanagedCallbackPtr);
    }

    public override void Dispose()
    {
        NativeNetMessages.RemoveNetMessageClientHook(_nativeListenerId);
    }

}

internal class NetMessageServerRawHookCallback : NetMessageHookCallback
{

    private INetMessageService.RawServerNetMessageHandler _callback;
    private NetMessageServerHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeListenerId;
    private ILogger<NetMessageServerRawHookCallback> _logger;
    private static readonly int CNetMsgOffset = NativeNetMessages.GetCNetMessageSize();

    public NetMessageServerRawHookCallback( INetMessageService.RawServerNetMessageHandler callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageServerRawHookCallback>();

        _callback = callback;

        _unmanagedCallback = ( pPlayerMask, msgId, pMessage ) =>
        {
            try
            {
                var msg = new UntypedNetMessage(pMessage + CNetMsgOffset, msgId);
                var result = _callback(msg, pPlayerMask);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
                _logger.LogError(e, "Error in raw server net message hook callback (msgId={MsgId})", msgId);
                return HookResult.Continue;
            }
        };
        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeListenerId = NativeNetMessages.AddNetMessageServerHook(_unmanagedCallbackPtr);
    }

    public override void Dispose()
    {
        NativeNetMessages.RemoveNetMessageServerHook(_nativeListenerId);
    }

}

internal class NetMessageServerInternalRawHookCallback : NetMessageHookCallback
{

    private INetMessageService.RawServerNetMessageInternalHandler _callback;
    private NetMessageClientHookCallbackDelegate _unmanagedCallback;
    private nint _unmanagedCallbackPtr;
    private ulong _nativeListenerId;
    private ILogger<NetMessageServerInternalRawHookCallback> _logger;
    private static readonly int CNetMsgOffset = NativeNetMessages.GetCNetMessageSize();

    public NetMessageServerInternalRawHookCallback( INetMessageService.RawServerNetMessageInternalHandler callback, ILoggerFactory loggerFactory, IContextedProfilerService profiler ) : base(loggerFactory, profiler)
    {
        Guid = Guid.NewGuid();
        _logger = LoggerFactory.CreateLogger<NetMessageServerInternalRawHookCallback>();

        _callback = callback;

        _unmanagedCallback = ( playerId, msgId, pMessage ) =>
        {
            try
            {
                var msg = new UntypedNetMessage(pMessage + CNetMsgOffset, msgId);
                var result = _callback(playerId, msg);
                return result;
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return HookResult.Continue;
                _logger.LogError(e, "Error in raw server net internal hook callback (msgId={MsgId})", msgId);
                return HookResult.Continue;
            }
        };
        _unmanagedCallbackPtr = Marshal.GetFunctionPointerForDelegate(_unmanagedCallback);
        _nativeListenerId = NativeNetMessages.AddNetMessageServerHookInternal(_unmanagedCallbackPtr);
    }

    public override void Dispose()
    {
        NativeNetMessages.RemoveNetMessageServerHookInternal(_nativeListenerId);
    }

}