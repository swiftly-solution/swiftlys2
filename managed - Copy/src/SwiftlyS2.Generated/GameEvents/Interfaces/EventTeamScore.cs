using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "team_score"
/// team score changed
/// </summary>
public interface EventTeamScore : IGameEvent<EventTeamScore> {

  static EventTeamScore IGameEvent<EventTeamScore>.Create(nint address) => new EventTeamScoreImpl(address);

  static string IGameEvent<EventTeamScore>.GetName() => "team_score";

  static uint IGameEvent<EventTeamScore>.GetHash() => 0x0E418BF1u;
  /// <summary>
  /// team id
  /// <br/>
  /// type: byte
  /// </summary>
  byte TeamID { get; set; }

  /// <summary>
  /// total team score
  /// <br/>
  /// type: short
  /// </summary>
  short Score { get; set; }

}
