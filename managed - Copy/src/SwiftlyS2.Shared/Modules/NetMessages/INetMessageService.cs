using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.NetMessages;

public interface INetMessageService {

  /// <summary>
  /// The handler to handle net messages that are sent from server to the client.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  /// <param name="msg">The net message to handle.</param>
  /// <param name="filter">The recipient filter for the net message.</param>
  /// <returns>The hook result.</returns>
  delegate HookResult ServerNetMessageHandler<T>(T msg) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// The handler to handle net messages that are sent from the client to the server.
  /// </summary>
  /// <typeparam name="T">Client net message type.</typeparam>
  /// <param name="msg">The net message to handle.</param>
  /// <param name="playerId">The player ID of the client that sent the net message.</param>
  /// <returns>The hook result.</returns>
  delegate HookResult ClientNetMessageHandler<T>(T msg, int playerId) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Hooks a client net message.
  /// </summary>
  /// <typeparam name="T">Client net message type.</typeparam>
  /// <param name="callback">The callback to handle the net message.</param>
  /// <returns>The unique Guid for the handler. Can be used to unhook it later.</returns>
  public Guid HookClientMessage<T>(ClientNetMessageHandler<T> callback) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Hooks a server net message.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  /// <param name="callback">The callback to handle the net message.</param>
  /// <returns>The unique Guid for the handler. Can be used to unhook it later.</returns>
  public Guid HookServerMessage<T>(ServerNetMessageHandler<T> callback) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Unhooks a net message handler.
  /// </summary>
  /// <param name="guid">The unique Guid for the handler.</param>
  public void Unhook(Guid guid);

  /// <summary>
  /// Unhooks all client net message handlers with specified type.
  /// </summary>
  /// <typeparam name="T">Client net message type.</typeparam>
  public void UnhookClientMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Unhooks all server net message handlers with specified type.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  public void UnhookServerMessage<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Creates a new net message of specified type.
  /// </summary>
  /// <typeparam name="T">Net message type.</typeparam>
  /// <returns>The new net message.</returns>
  public T Create<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Sends a net message to players with configured recipient filter.
  /// </summary>
  /// <typeparam name="T">Net message type.</typeparam>
  /// <param name="configureMessage">The action to configure the net message and recipient filter.</param>
  public void Send<T>(Action<T> configureMessage) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;
}