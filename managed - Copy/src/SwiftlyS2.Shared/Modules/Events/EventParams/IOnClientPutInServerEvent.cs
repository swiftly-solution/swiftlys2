namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client is put in the server.
/// </summary>
public interface IOnClientPutInServerEvent {

  /// <summary>
  /// The player ID of the client that was put in the server.
  /// </summary>
  public int PlayerId { get; }

  /// <summary>
  /// The kind of client that was put in the server.
  /// </summary>
  public ClientKind Kind { get; }
} 