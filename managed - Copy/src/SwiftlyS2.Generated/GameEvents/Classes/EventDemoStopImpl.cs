using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "demo_stop"
/// </summary>
internal class EventDemoStopImpl : GameEvent<EventDemoStop>, EventDemoStop
{

  public EventDemoStopImpl(nint address) : base(address)
  {
  }
}
