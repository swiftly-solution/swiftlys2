using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cart_updated"
/// </summary>
public interface EventCartUpdated : IGameEvent<EventCartUpdated> {

  static EventCartUpdated IGameEvent<EventCartUpdated>.Create(nint address) => new EventCartUpdatedImpl(address);

  static string IGameEvent<EventCartUpdated>.GetName() => "cart_updated";

  static uint IGameEvent<EventCartUpdated>.GetHash() => 0x3A4BF24Fu;
}
