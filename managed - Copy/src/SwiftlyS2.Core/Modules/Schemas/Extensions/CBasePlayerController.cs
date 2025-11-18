namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBasePlayerController
{
    /// <summary>
    /// Sets the player pawn to the entity.
    /// </summary>
    /// <param name="pawn">The player pawn to associate. Can be null to remove the current association.</param>
    public void SetPawn(CBasePlayerPawn? pawn);
}