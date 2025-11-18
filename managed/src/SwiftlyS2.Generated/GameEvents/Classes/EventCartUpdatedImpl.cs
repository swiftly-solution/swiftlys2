using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cart_updated"
/// </summary>
internal class EventCartUpdatedImpl : GameEvent<EventCartUpdated>, EventCartUpdated
{

    public EventCartUpdatedImpl( nint address ) : base(address)
    {
    }
}
