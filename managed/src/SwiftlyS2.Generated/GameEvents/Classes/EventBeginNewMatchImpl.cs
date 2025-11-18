using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "begin_new_match"
/// </summary>
internal class EventBeginNewMatchImpl : GameEvent<EventBeginNewMatch>, EventBeginNewMatch
{

    public EventBeginNewMatchImpl( nint address ) : base(address)
    {
    }
}
