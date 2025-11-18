using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Profiler;

namespace SwiftlyS2.Core.NetMessages;


internal class NetMessageService : INetMessageService, IDisposable
{

  private List<NetMessageHookCallback> _callbacks = new();
  private ILoggerFactory _loggerFactory;
  private IContextedProfilerService _profiler;
  private Lock _lock = new();


  public NetMessageService( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
  {
    _loggerFactory = loggerFactory;
    _profiler = profiler;
  }

  public Guid HookClientMessage<T>( INetMessageService.ClientNetMessageHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
  {
    var hook = new NetMessageClientHookCallback<T>(callback, _loggerFactory, _profiler);
    lock (_lock)
    {
      _callbacks.Add(hook);
    }
    return hook.Guid;
  }

  public Guid HookServerMessage<T>( INetMessageService.ServerNetMessageHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
  {
    var hook = new NetMessageServerHookCallback<T>(callback, _loggerFactory, _profiler);
    lock (_lock)
    {
      _callbacks.Add(hook);
    }
    return hook.Guid;
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

  public void UnhookClientMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback is NetMessageClientHookCallback<T> clientHook)
        {
          clientHook.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public void UnhookServerMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback is NetMessageServerHookCallback<T> serverHook)
        {
          serverHook.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  private nint AllocateNetMessage( int msgId )
  {
    var handle = NativeNetMessages.AllocateNetMessageByID(msgId);
    if (handle == 0) throw new InvalidOperationException("Failed to allocate net message. This is possibly caused by the message ID is already deprecated not supported in game.");
    return handle;
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
    NativeNetMessages.SendMessageToPlayers(handle, T.MessageId, message.Recipients.ToMask());
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