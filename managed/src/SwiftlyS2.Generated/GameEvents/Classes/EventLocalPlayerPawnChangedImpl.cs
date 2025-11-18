using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "local_player_pawn_changed"
/// </summary>
internal class EventLocalPlayerPawnChangedImpl : GameEvent<EventLocalPlayerPawnChanged>, EventLocalPlayerPawnChanged
{

    public EventLocalPlayerPawnChangedImpl( nint address ) : base(address)
    {
    }
}
