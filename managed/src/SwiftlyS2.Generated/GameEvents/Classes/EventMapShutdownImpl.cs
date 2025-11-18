using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "map_shutdown"
/// </summary>
internal class EventMapShutdownImpl : GameEvent<EventMapShutdown>, EventMapShutdown
{

    public EventMapShutdownImpl( nint address ) : base(address)
    {
    }
}
