using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.NetMessages;

public interface INetMessageService
{

  /// <summary>
  /// The handler to handle net messages that are sent from server to the client.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  /// <param name="msg">The net message to handle.</param>
  /// <returns>The hook result.</returns>
  delegate HookResult ServerNetMessageHandler<T>( T msg ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// The handler to handle net messages that are sent from server to the client.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  /// <param name="msg">The net message to handle.</param>
  /// <param name="playerId">The recipient filter for the net message.</param>
  /// <returns>The hook result.</returns>
  delegate HookResult ServerNetMessageInternalHandler<T>( T msg, int playerId ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// The handler to handle net messages that are sent from the client to the server.
  /// </summary>
  /// <typeparam name="T">Client net message type.</typeparam>
  /// <param name="msg">The net message to handle.</param>
  /// <param name="playerId">The player ID of the client that sent the net message.</param>
  /// <returns>The hook result.</returns>
  delegate HookResult ClientNetMessageHandler<T>( T msg, int playerId ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  // ── Raw (untyped) handlers ──────────────────────────────────────

  /// <summary>
  /// Raw handler for ALL client net messages (untyped).
  /// </summary>
  /// <param name="playerId">The player that sent the message.</param>
  /// <param name="msg">The untyped net message with MessageId, MessageName, Accessor, and GetDebugString().</param>
  /// <returns>The hook result.</returns>
  delegate HookResult RawClientNetMessageHandler( int playerId, IUntypedNetMessage msg );

  /// <summary>
  /// Raw handler for ALL server net messages (untyped).
  /// </summary>
  /// <param name="msg">The untyped net message with MessageId, MessageName, Accessor, and GetDebugString().</param>
  /// <param name="pPlayerMask">Pointer to a uint64 bitmap of recipients.</param>
  /// <returns>The hook result.</returns>
  delegate HookResult RawServerNetMessageHandler( IUntypedNetMessage msg, nint pPlayerMask );

  /// <summary>
  /// Raw handler for ALL server net internal messages (untyped).
  /// </summary>
  /// <param name="playerId">The target player ID.</param>
  /// <param name="msg">The untyped net message with MessageId, MessageName, Accessor, and GetDebugString().</param>
  /// <returns>The hook result.</returns>
  delegate HookResult RawServerNetMessageInternalHandler( int playerId, IUntypedNetMessage msg );

  /// <summary>
  /// Get the unscoped name of a net message by its ID.
  /// </summary>
  /// <param name="msgId">The net message ID.</param>
  /// <returns>The unscoped message name, or empty string if not found.</returns>
  public string GetMessageNameById( int msgId );

  // ── Typed hooks ──────────────────────────────────────────────────

  /// <summary>
  /// Hooks a client net message.
  /// </summary>
  /// <typeparam name="T">Client net message type.</typeparam>
  /// <param name="callback">The callback to handle the net message.</param>
  /// <returns>The unique Guid for the handler. Can be used to unhook it later.</returns>
  public Guid HookClientMessage<T>( ClientNetMessageHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Hooks a server net message.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  /// <param name="callback">The callback to handle the net message.</param>
  /// <returns>The unique Guid for the handler. Can be used to unhook it later.</returns>
  public Guid HookServerMessage<T>( ServerNetMessageHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  /// <summary>
  /// Hooks a client net message internally.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  /// <param name="callback"></param>
  /// <returns></returns>
  public Guid HookServerMessageInternal<T>( ServerNetMessageInternalHandler<T> callback ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

  // ── Raw hooks (untyped — catch ALL messages) ─────────────────────

  /// <summary>
  /// Hooks ALL client net messages without type filtering.
  /// </summary>
  /// <param name="callback">
  /// Called for every incoming client → server message.
  /// The message is encapsulated in <see cref="IUntypedNetMessage"/> which provides
  /// MessageId, MessageName, an <see cref="IProtobufAccessor"/> for field access, and GetDebugString().
  /// </param>
  /// <returns>Hook identifier for later unhook.</returns>
  public Guid HookClientMessageRaw( RawClientNetMessageHandler callback );

  /// <summary>
  /// Hooks ALL server net messages without type filtering.
  /// </summary>
  /// <param name="callback">
  /// Called for every outgoing server → client broadcast message.
  /// The message is encapsulated in <see cref="IUntypedNetMessage"/>.
  /// </param>
  /// <returns>Hook identifier for later unhook.</returns>
  public Guid HookServerMessageRaw( RawServerNetMessageHandler callback );

  /// <summary>
  /// Hooks ALL server net internal (unicast) messages without type filtering.
  /// </summary>
  /// <param name="callback">
  /// Called for every outgoing server → client unicast message.
  /// The message is encapsulated in <see cref="IUntypedNetMessage"/>.
  /// </param>
  /// <returns>Hook identifier for later unhook.</returns>
  public Guid HookServerMessageInternalRaw( RawServerNetMessageInternalHandler callback );

  /// <summary>
  /// Unhooks a net message handler.
  /// </summary>
  /// <param name="guid">The unique Guid for the handler.</param>
  public void Unhook( Guid guid );

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
  /// Unhooks all internal server net message handlers with specified type.
  /// </summary>
  /// <typeparam name="T">Server net message type.</typeparam>
  public void UnhookServerMessageInternal<T>() where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;

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
  public void Send<T>( Action<T> configureMessage ) where T : ITypedProtobuf<T>, INetMessage<T>, IDisposable;
}

public interface IUntypedNetMessage
{
    int MessageId { get; }
    string MessageName { get; }
    IProtobufAccessor Accessor { get; }
    string GetDebugString();
}