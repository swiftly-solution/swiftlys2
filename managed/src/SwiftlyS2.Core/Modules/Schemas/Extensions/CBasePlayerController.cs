using System.Diagnostics.CodeAnalysis;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBasePlayerController
{
    /// <summary>
    /// Sets the player pawn to the entity.
    /// </summary>
    /// <param name="pawn">The player pawn to associate. Can be null to remove the current association.</param>
    public void SetPawn( CBasePlayerPawn? pawn );

    /// <summary>
    /// Converts the controller to a player.
    /// </summary>
    /// <returns>An <see cref="IPlayer"/> instance representing the player with the specified controller, or <c>null</c> if no such
    /// player exists.</returns>
    public IPlayer? ToPlayer();

}