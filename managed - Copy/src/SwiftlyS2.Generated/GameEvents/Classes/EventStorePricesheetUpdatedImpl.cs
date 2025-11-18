using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "store_pricesheet_updated"
/// </summary>
internal class EventStorePricesheetUpdatedImpl : GameEvent<EventStorePricesheetUpdated>, EventStorePricesheetUpdated
{

  public EventStorePricesheetUpdatedImpl(nint address) : base(address)
  {
  }
}
