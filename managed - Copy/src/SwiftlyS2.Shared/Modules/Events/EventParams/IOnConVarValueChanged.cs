namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a ConVar value is changed.
/// </summary>
public interface IOnConVarValueChanged
{
    /// <summary>
    /// The name of the ConVar that changed.
    /// </summary>
    public string ConVarName { get; }

    /// <summary>
    /// The player ID of the client that made the change.
    /// </summary>
    public int PlayerId { get; }

    /// <summary>
    /// The old value of the ConVar in string format.
    /// </summary>
    public string NewValue { get; }

    /// <summary>
    /// The new value of the ConVar in string format.
    /// </summary>
    public string OldValue { get; }
}