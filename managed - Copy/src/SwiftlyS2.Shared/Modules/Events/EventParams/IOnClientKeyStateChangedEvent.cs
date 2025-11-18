namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client's key state changes.
/// </summary>
public interface IOnClientKeyStateChangedEvent {

  /// <summary>
  /// The player ID of the client that changed their key state.
  /// </summary>
  public int PlayerId { get; }

  /// <summary>
  /// The key that was pressed or released.
  /// </summary>
  public KeyKind Key { get; }

  /// <summary>
  /// Whether the key was pressed or released.
  /// </summary>
  public bool Pressed { get; }
} 