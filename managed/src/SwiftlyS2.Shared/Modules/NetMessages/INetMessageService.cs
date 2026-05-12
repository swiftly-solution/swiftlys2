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
  /// Receives the message as a google::protobuf::Message* pointer (already offset from CNetMessage*).
  /// Use <see cref="GetMessageNameById"/> to resolve the message name,
  /// and <see cref="SwiftlyS2.Shared.Memory.IMemoryService.DebugProtobuf"/> to get its DebugString.
  /// </summary>
  delegate HookResult RawClientNetMessageHandler( int playerId, int msgId, nint pMessage );

  /// <summary>
  /// Raw handler for ALL server net messages (untyped).
  /// pPlayerMask is a pointer to a uint64 bitmap of recipients.
  /// pMessage is a google::protobuf::Message* pointer (already offset from CNetMessage*).
  /// </summary>
  delegate HookResult RawServerNetMessageHandler( nint pPlayerMask, int msgId, nint pMessage );

  /// <summary>
  /// Raw handler for ALL server net internal messages (untyped).
  /// pMessage is a google::protobuf::Message* pointer (already offset from CNetMessage*).
  /// </summary>
  delegate HookResult RawServerNetMessageInternalHandler( int playerId, int msgId, nint pMessage );

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
  /// Parameters: playerId, msgId, pMessage (raw protobuf pointer).
  /// </param>
  /// <returns>Hook identifier for later unhook.</returns>
  public Guid HookClientMessageRaw( RawClientNetMessageHandler callback );

  /// <summary>
  /// Hooks ALL server net messages without type filtering.
  /// </summary>
  /// <param name="callback">
  /// Called for every outgoing server → client broadcast message.
  /// Parameters: pPlayerMask (uint64* recipients bitmap), msgId, pMessage.
  /// </param>
  /// <returns>Hook identifier for later unhook.</returns>
  public Guid HookServerMessageRaw( RawServerNetMessageHandler callback );

  /// <summary>
  /// Hooks ALL server net internal (unicast) messages without type filtering.
  /// </summary>
  /// <param name="callback">
  /// Called for every outgoing server → client unicast message.
  /// Parameters: playerId, msgId, pMessage.
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