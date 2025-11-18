using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "start_halftime"
/// </summary>
internal class EventStartHalftimeImpl : GameEvent<EventStartHalftime>, EventStartHalftime
{

  public EventStartHalftimeImpl(nint address) : base(address)
  {
  }
}
