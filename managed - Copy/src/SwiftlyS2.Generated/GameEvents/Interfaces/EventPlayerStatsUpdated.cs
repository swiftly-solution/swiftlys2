using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_stats_updated"
/// </summary>
public interface EventPlayerStatsUpdated : IGameEvent<EventPlayerStatsUpdated> {

  static EventPlayerStatsUpdated IGameEvent<EventPlayerStatsUpdated>.Create(nint address) => new EventPlayerStatsUpdatedImpl(address);

  static string IGameEvent<EventPlayerStatsUpdated>.GetName() => "player_stats_updated";

  static uint IGameEvent<EventPlayerStatsUpdated>.GetHash() => 0x9F0E110Cu;
  /// <summary>
  /// type: bool
  /// </summary>
  bool ForceUpload { get; set; }

}
