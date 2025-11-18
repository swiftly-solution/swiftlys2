using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "map_shutdown"
/// </summary>
internal class EventMapShutdownImpl : GameEvent<EventMapShutdown>, EventMapShutdown
{

  public EventMapShutdownImpl(nint address) : base(address)
  {
  }
}
