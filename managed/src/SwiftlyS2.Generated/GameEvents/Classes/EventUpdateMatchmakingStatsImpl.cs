using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "update_matchmaking_stats"
/// </summary>
internal class EventUpdateMatchmakingStatsImpl : GameEvent<EventUpdateMatchmakingStats>, EventUpdateMatchmakingStats
{

    public EventUpdateMatchmakingStatsImpl( nint address ) : base(address)
    {
    }
}
