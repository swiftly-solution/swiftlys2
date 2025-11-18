using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CCSGameRules
{
    /// <summary>
    /// Find the player that the controller is targetting
    /// </summary>
    /// <typeparam name="T">Entity Class</typeparam>
    /// <param name="controller">Player Controller</param>
    public T? FindPickerEntity<T>( CBasePlayerController controller ) where T : ISchemaClass<T>;

    /// <summary>
    /// Ends the current round with the specified reason after an optional delay
    /// </summary>
    /// <param name="reason">The reason for ending the round</param>
    /// <param name="delay">The delay before ending the round</param>
    public void TerminateRound( RoundEndReason reason, float delay );
}