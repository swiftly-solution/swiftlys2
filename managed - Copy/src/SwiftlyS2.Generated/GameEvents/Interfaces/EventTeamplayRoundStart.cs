using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "teamplay_round_start"
/// round restart
/// </summary>
public interface EventTeamplayRoundStart : IGameEvent<EventTeamplayRoundStart> {

  static EventTeamplayRoundStart IGameEvent<EventTeamplayRoundStart>.Create(nint address) => new EventTeamplayRoundStartImpl(address);

  static string IGameEvent<EventTeamplayRoundStart>.GetName() => "teamplay_round_start";

  static uint IGameEvent<EventTeamplayRoundStart>.GetHash() => 0xB3DC0DA2u;
  /// <summary>
  /// is this a full reset of the map
  /// <br/>
  /// type: bool
  /// </summary>
  bool FullReset { get; set; }

}
