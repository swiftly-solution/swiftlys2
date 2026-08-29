using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary>
/// Event "core game events"
/// </summary>
public interface EventCoreGameEvents : IGameEvent<EventCoreGameEvents>
{

    static EventCoreGameEvents IGameEvent<EventCoreGameEvents>.Create(nint address) => new EventCoreGameEventsImpl(address);

    static string IGameEvent<EventCoreGameEvents>.GetName() => "core game events";

    static uint IGameEvent<EventCoreGameEvents>.GetHash() => 0x0A5D8AB9u;
}
