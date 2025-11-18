using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "local_player_team"
/// </summary>
internal class EventLocalPlayerTeamImpl : GameEvent<EventLocalPlayerTeam>, EventLocalPlayerTeam
{

    public EventLocalPlayerTeamImpl( nint address ) : base(address)
    {
    }
}
