using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "gc_connected"
/// </summary>
internal class EventGcConnectedImpl : GameEvent<EventGcConnected>, EventGcConnected
{

    public EventGcConnectedImpl( nint address ) : base(address)
    {
    }
}
