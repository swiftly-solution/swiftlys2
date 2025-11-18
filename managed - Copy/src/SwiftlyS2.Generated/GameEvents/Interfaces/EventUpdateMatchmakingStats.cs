using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "update_matchmaking_stats"
/// </summary>
public interface EventUpdateMatchmakingStats : IGameEvent<EventUpdateMatchmakingStats> {

  static EventUpdateMatchmakingStats IGameEvent<EventUpdateMatchmakingStats>.Create(nint address) => new EventUpdateMatchmakingStatsImpl(address);

  static string IGameEvent<EventUpdateMatchmakingStats>.GetName() => "update_matchmaking_stats";

  static uint IGameEvent<EventUpdateMatchmakingStats>.GetHash() => 0xFB94AEE7u;
}
