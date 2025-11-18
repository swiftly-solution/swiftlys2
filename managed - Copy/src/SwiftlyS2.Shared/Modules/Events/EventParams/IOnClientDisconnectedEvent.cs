using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client disconnects from the server.
/// </summary>
public interface IOnClientDisconnectedEvent {

  /// <summary>
  /// The player ID of the client that disconnected.
  /// </summary>
  public int PlayerId { get; }


  /// <summary>
  /// The reason for the client to disconnect.
  /// </summary>
  public ENetworkDisconnectionReason Reason { get; }
} 