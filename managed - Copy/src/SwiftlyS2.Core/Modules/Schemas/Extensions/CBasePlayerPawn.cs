namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBasePlayerPawn
{
    /// <summary>
    /// Performs a suicide on the pawn, optionally causing an explosion and allowing forced execution.
    /// </summary>
    public void CommitSuicide(bool explode, bool force);
}