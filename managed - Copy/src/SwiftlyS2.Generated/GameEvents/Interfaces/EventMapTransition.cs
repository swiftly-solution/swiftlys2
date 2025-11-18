using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "map_transition"
/// </summary>
public interface EventMapTransition : IGameEvent<EventMapTransition> {

  static EventMapTransition IGameEvent<EventMapTransition>.Create(nint address) => new EventMapTransitionImpl(address);

  static string IGameEvent<EventMapTransition>.GetName() => "map_transition";

  static uint IGameEvent<EventMapTransition>.GetHash() => 0x75B82729u;
}
