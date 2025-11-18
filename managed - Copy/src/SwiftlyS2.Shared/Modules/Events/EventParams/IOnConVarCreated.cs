namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a ConVar is created.
/// </summary>
public interface IOnConVarCreated
{
    /// <summary>
    /// The name of the ConVar that was created.
    /// </summary>
    public string ConVarName { get; }
}