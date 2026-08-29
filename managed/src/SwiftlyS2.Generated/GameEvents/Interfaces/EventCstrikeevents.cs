using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary>
/// Event "cstrikeevents"
/// </summary>
public interface EventCstrikeevents : IGameEvent<EventCstrikeevents>
{

    static EventCstrikeevents IGameEvent<EventCstrikeevents>.Create(nint address) => new EventCstrikeeventsImpl(address);

    static string IGameEvent<EventCstrikeevents>.GetName() => "cstrikeevents";

    static uint IGameEvent<EventCstrikeevents>.GetHash() => 0xA558D2C9u;
}
