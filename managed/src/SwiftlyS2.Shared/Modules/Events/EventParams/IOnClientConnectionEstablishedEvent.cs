namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client connection is established.
/// </summary>
public interface IOnClientConnectionEstablishedEvent
{
    /// <summary>
    /// The player ID of the client.
    /// </summary>
    public int PlayerId { get; }
}
