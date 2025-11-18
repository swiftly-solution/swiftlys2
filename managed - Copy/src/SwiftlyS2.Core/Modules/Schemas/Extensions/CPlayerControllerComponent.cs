namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CPlayerControllerComponent
{
    /// <summary>
    /// Gets the player controller associated with this instance.
    /// </summary>
    public CBasePlayerController Controller { get; }
}