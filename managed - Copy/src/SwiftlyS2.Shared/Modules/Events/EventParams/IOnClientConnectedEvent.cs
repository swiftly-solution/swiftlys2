using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client connects to the server.
/// </summary>
public interface IOnClientConnectedEvent {

  /// <summary>
  /// The player ID of the client that connected.
  /// </summary>
  public int PlayerId { get; }

  /// <summary>
  /// The result of the event.
  /// Set this to <see cref="HookResult.Stop"/> to prevent player from joining in.
  /// </summary>
  public HookResult Result { get; set; }
  
}
