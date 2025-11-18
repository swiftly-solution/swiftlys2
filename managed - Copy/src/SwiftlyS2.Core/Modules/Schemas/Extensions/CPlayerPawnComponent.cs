namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CPlayerPawnComponent
{
    /// <summary>
    /// Gets the player pawn associated with this instance.
    /// </summary>
    public CBasePlayerPawn Pawn { get; }
}