using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary>
/// Event "core game events"
/// </summary>
internal class EventCoreGameEventsImpl : GameEvent<EventCoreGameEvents>, EventCoreGameEvents
{

    public EventCoreGameEventsImpl(nint address) : base(address)
    {
    }
}
