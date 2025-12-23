using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBasePlayerPawn
{
    public Vector? EyePosition { get; }
    public float GroundDistance { get; }
    public MaskTrace InteractsWith { get; }
    public MaskTrace InteractsAs { get; }
    public MaskTrace InteractsExclude { get; }

    /// <summary>
    /// Performs a suicide on the pawn, optionally causing an explosion and allowing forced execution.
    /// </summary>
    public void CommitSuicide( bool explode, bool force );

    /// <summary>
    /// Checks if the target player is within the line of sight of this player.
    /// Performs both physical obstruction check and field of view validation.
    /// </summary>
    /// <param name="targetPlayer">The target player to check visibility for.</param>
    /// <param name="fieldOfViewDegrees">Optional field of view in degrees.</param>
    /// <returns>True if the target player is visible; otherwise, false.</returns>
    public bool HasLineOfSight( CCSPlayerPawn targetPlayer, float? fieldOfViewDegrees = null );

    /// <summary>
    /// Converts the pawn to a player.
    /// </summary>
    /// <returns>An <see cref="IPlayer"/> instance representing the player with the specified pawn, or <c>null</c> if no such
    /// player exists.</returns>
    public IPlayer? ToPlayer();
}