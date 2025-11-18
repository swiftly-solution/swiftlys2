using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "local_player_controller_team"
/// </summary>
public interface EventLocalPlayerControllerTeam : IGameEvent<EventLocalPlayerControllerTeam>
{

    static EventLocalPlayerControllerTeam IGameEvent<EventLocalPlayerControllerTeam>.Create( nint address ) => new EventLocalPlayerControllerTeamImpl(address);

    static string IGameEvent<EventLocalPlayerControllerTeam>.GetName() => "local_player_controller_team";

    static uint IGameEvent<EventLocalPlayerControllerTeam>.GetHash() => 0x06A77413u;
}
