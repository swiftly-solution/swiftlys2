using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CCSGameRules
{
    public GamePhase GamePhaseEnum { get; set; }

    /// <summary>
    /// Find the player that the controller is targetting
    /// </summary>
    /// <typeparam name="T">Entity Class</typeparam>
    /// <param name="controller">Player Controller</param>
    public T? FindPickerEntity<T>( CBasePlayerController controller ) where T : class, ISchemaClass<T>;

    /// <summary>
    /// Ends the current round with the specified reason after an optional delay
    /// </summary>
    /// <param name="reason">The reason for ending the round</param>
    /// <param name="delay">The delay before ending the round</param>
    public void TerminateRound( RoundEndReason reason, float delay );

    /// <summary>
    /// Ends the current round with the specified reason after an optional delay
    /// </summary>
    /// <param name="reason">The reason for ending the round</param>
    /// <param name="delay">The delay before ending the round</param>
    /// <param name="teamId">The team id to end the round for</param>
    /// <param name="unk01">Unknown parameter</param>
    public void TerminateRound( RoundEndReason reason, float delay, uint teamId, uint unk01 = 0 );
    
    /// <summary>
    /// Go to the intermission phase of the game.
    /// </summary>
    /// <param name="bAbortedMatch">Indicates whether the match was aborted</param>
    public void GoToIntermission(bool bAbortedMatch = false);

    /// <summary>
    /// 
    /// Get the global view vectors.
    /// </summary>
    /// <returns>The view vectors.</returns>
    public ref CViewVectors GetViewVectors();
}