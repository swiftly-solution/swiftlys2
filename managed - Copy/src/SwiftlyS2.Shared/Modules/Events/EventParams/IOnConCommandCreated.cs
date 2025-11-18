namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a ConVar is created.
/// </summary>
public interface IOnConCommandCreated
{
    /// <summary>
    /// The name of the ConVar that was created.
    /// </summary>
    public string CommandName { get; }
}