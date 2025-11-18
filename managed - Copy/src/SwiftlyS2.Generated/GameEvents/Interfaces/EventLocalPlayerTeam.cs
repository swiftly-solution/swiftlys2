using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "local_player_team"
/// </summary>
public interface EventLocalPlayerTeam : IGameEvent<EventLocalPlayerTeam> {

  static EventLocalPlayerTeam IGameEvent<EventLocalPlayerTeam>.Create(nint address) => new EventLocalPlayerTeamImpl(address);

  static string IGameEvent<EventLocalPlayerTeam>.GetName() => "local_player_team";

  static uint IGameEvent<EventLocalPlayerTeam>.GetHash() => 0x04FD6AB4u;
}
