using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "map_transition"
/// </summary>
internal class EventMapTransitionImpl : GameEvent<EventMapTransition>, EventMapTransition
{

  public EventMapTransitionImpl(nint address) : base(address)
  {
  }
}
