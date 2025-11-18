using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_init"
/// sent when a new game is started
/// </summary>
internal class EventGameInitImpl : GameEvent<EventGameInit>, EventGameInit
{

    public EventGameInitImpl( nint address ) : base(address)
    {
    }
}
