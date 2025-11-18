using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "store_pricesheet_updated"
/// </summary>
public interface EventStorePricesheetUpdated : IGameEvent<EventStorePricesheetUpdated> {

  static EventStorePricesheetUpdated IGameEvent<EventStorePricesheetUpdated>.Create(nint address) => new EventStorePricesheetUpdatedImpl(address);

  static string IGameEvent<EventStorePricesheetUpdated>.GetName() => "store_pricesheet_updated";

  static uint IGameEvent<EventStorePricesheetUpdated>.GetHash() => 0xE425C0FFu;
}
