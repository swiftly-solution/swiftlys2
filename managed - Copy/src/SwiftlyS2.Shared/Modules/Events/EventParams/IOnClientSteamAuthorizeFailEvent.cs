namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client's Steam authorization fails.
/// </summary>
public interface IOnClientSteamAuthorizeFailEvent {

  /// <summary>
  /// The player ID of the client that failed to authorize.
  /// </summary>
  public int PlayerId { get; }
} 