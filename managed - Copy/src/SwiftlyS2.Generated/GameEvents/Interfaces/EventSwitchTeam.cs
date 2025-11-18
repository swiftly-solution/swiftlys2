using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "switch_team"
/// </summary>
public interface EventSwitchTeam : IGameEvent<EventSwitchTeam> {

  static EventSwitchTeam IGameEvent<EventSwitchTeam>.Create(nint address) => new EventSwitchTeamImpl(address);

  static string IGameEvent<EventSwitchTeam>.GetName() => "switch_team";

  static uint IGameEvent<EventSwitchTeam>.GetHash() => 0x53717ECBu;
  /// <summary>
  /// number of active players on both T and CT
  /// <br/>
  /// type: short
  /// </summary>
  short NumPlayers { get; set; }

  /// <summary>
  /// number of spectators
  /// <br/>
  /// type: short
  /// </summary>
  short NumSpectators { get; set; }

  /// <summary>
  /// average rank of human players
  /// <br/>
  /// type: short
  /// </summary>
  short AvgRank { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short NumTSlotsFree { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short NumCTSlotsFree { get; set; }

}
