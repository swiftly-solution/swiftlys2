namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client changed one or several replicated convars (name etc).
/// </summary>
public interface IOnClientSettingsChangedEvent
{
    /// <summary>
    /// The player ID of the client whose settings changed.
    /// </summary>
    public int PlayerId { get; }
}
