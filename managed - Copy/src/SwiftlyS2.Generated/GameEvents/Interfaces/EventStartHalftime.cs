using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "start_halftime"
/// </summary>
public interface EventStartHalftime : IGameEvent<EventStartHalftime> {

  static EventStartHalftime IGameEvent<EventStartHalftime>.Create(nint address) => new EventStartHalftimeImpl(address);

  static string IGameEvent<EventStartHalftime>.GetName() => "start_halftime";

  static uint IGameEvent<EventStartHalftime>.GetHash() => 0xC0794EE0u;
}
