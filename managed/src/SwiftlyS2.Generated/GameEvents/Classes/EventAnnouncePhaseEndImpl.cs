using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "announce_phase_end"
/// </summary>
internal class EventAnnouncePhaseEndImpl : GameEvent<EventAnnouncePhaseEnd>, EventAnnouncePhaseEnd
{

    public EventAnnouncePhaseEndImpl( nint address ) : base(address)
    {
    }
}
