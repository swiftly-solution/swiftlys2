using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.NetMessages;


internal class NetMessageService : INetMessageService, IDisposable
{

    private List<NetMessageHookCallback> _callbacks = [];
    private ILoggerFactory _loggerFactory;
    private IContextedProfilerService _profiler;
    private Lock _lock = new();


    public NetMessageService( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
    {
        _loggerFactory = loggerFactory;
        _profiler = profiler;
    }

    public Guid HookClientMessage<T>( INetMessageService.ClientNetMessageHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable =>
        AddCallback(new NetMessageClientHookCallback<T>(callback, _loggerFactory, _profiler));

    public Guid HookServerMessage<T>( INetMessageService.ServerNetMessageHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable =>
        AddCallback(new NetMessageServerHookCallback<T>(callback, _loggerFactory, _profiler));

    public Guid HookServerMessageInternal<T>( INetMessageService.ServerNetMessageInternalHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable =>
        AddCallback(new NetMessageServerInternalHookCallback<T>(callback, _loggerFactory, _profiler));

    public void Unhook( Guid guid ) => RemoveCallbacks(callback => callback.Guid == guid);

    public void UnhookClientMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable =>
        RemoveCallbacks(callback => callback is NetMessageClientHookCallback<T>);

    public void UnhookServerMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable =>
        RemoveCallbacks(callback => callback is NetMessageServerHookCallback<T>);

    public void UnhookServerMessageInternal<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable =>
        RemoveCallbacks(callback => callback is NetMessageServerInternalHookCallback<T>);

    private Guid AddCallback( NetMessageHookCallback callback )
    {
        lock (_lock)
        {
            _callbacks.Add(callback);
            NetMessageDispatcher.Register(callback);
        }

        return callback.Guid;
    }

    private void RemoveCallbacks( Predicate<NetMessageHookCallback> match )
    {
        lock (_lock)
        {
            foreach (var callback in _callbacks.FindAll(match))
            {
                NetMessageDispatcher.Unregister(callback);
            }

            _ = _callbacks.RemoveAll(match);
        }
    }

    private nint AllocateNetMessage( int msgId )
    {
        var handle = NativeNetMessages.AllocateNetMessageByID(msgId);
        return handle == 0
        ? throw new InvalidOperationException("Failed to allocate net message. This is possibly caused by the message ID is already deprecated not supported in game.")
        : handle;
    }

    public T Create<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
    {
        var handle = AllocateNetMessage(T.MessageId);
        var message = T.Wrap(handle, true);
        return message;
    }

    public void Send<T>( Action<T> configureMessage ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
    {
        var handle = AllocateNetMessage(T.MessageId);
        var message = T.Wrap(handle, true);
        configureMessage(message);
        message.Send();
    }

    public void Dispose()
    {
        RemoveCallbacks(static _ => true);
    }
}
