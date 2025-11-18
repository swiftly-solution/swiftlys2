using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Profiler;

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

    private readonly INetMessageService.ClientNetMessageHandler<T> _callback;
    private readonly NetMessageClientHookCallbackDelegate _unmanagedCallback;
    private readonly nint _unmanagedCallbackPtr;
    private readonly ulong _nativeListenerId;
    private readonly ILogger<NetMessageClientHookCallback<T>> _logger;


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
                if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
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

    private readonly INetMessageService.ServerNetMessageHandler<T> _callback;
    private readonly NetMessageServerHookCallbackDelegate _unmanagedCallback;
    private readonly nint _unmanagedCallbackPtr;
    private readonly ulong _nativeListenerId;
    private readonly ILogger<NetMessageServerHookCallback<T>> _logger;

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
                if (!GlobalExceptionHandler.Handle(e)) return HookResult.Continue;
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