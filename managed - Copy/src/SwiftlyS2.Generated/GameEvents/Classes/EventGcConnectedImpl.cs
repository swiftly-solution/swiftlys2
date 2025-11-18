using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "gc_connected"
/// </summary>
internal class EventGcConnectedImpl : GameEvent<EventGcConnected>, EventGcConnected
{

  public EventGcConnectedImpl(nint address) : base(address)
  {
  }
}
