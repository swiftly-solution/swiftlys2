namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client is authorized via Steam.
/// </summary>
public interface IOnClientSteamAuthorizeEvent {

  /// <summary>
  /// The player ID of the client that was authorized.
  /// </summary>
  public int PlayerId { get; }
} 