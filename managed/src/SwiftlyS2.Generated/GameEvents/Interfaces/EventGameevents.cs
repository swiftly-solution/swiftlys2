using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary>
/// Event "gameevents"
/// </summary>
public interface EventGameevents : IGameEvent<EventGameevents>
{

    static EventGameevents IGameEvent<EventGameevents>.Create(nint address) => new EventGameeventsImpl(address);

    static string IGameEvent<EventGameevents>.GetName() => "gameevents";

    static uint IGameEvent<EventGameevents>.GetHash() => 0xD80BD822u;
}
