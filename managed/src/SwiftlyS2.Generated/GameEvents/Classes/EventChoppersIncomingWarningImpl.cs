using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "choppers_incoming_warning"
/// </summary>
internal class EventChoppersIncomingWarningImpl : GameEvent<EventChoppersIncomingWarning>, EventChoppersIncomingWarning
{

    public EventChoppersIncomingWarningImpl( nint address ) : base(address)
    {
    }

    public bool Global { get => Accessor.GetBool("global"); set => Accessor.SetBool("global", value); }
}
