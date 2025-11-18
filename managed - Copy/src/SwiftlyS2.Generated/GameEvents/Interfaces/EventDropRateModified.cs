using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "drop_rate_modified"
/// </summary>
public interface EventDropRateModified : IGameEvent<EventDropRateModified> {

  static EventDropRateModified IGameEvent<EventDropRateModified>.Create(nint address) => new EventDropRateModifiedImpl(address);

  static string IGameEvent<EventDropRateModified>.GetName() => "drop_rate_modified";

  static uint IGameEvent<EventDropRateModified>.GetHash() => 0x4D5A279Bu;
}
