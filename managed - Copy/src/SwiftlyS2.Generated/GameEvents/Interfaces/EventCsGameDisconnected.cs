using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_game_disconnected"
/// </summary>
public interface EventCsGameDisconnected : IGameEvent<EventCsGameDisconnected> {

  static EventCsGameDisconnected IGameEvent<EventCsGameDisconnected>.Create(nint address) => new EventCsGameDisconnectedImpl(address);

  static string IGameEvent<EventCsGameDisconnected>.GetName() => "cs_game_disconnected";

  static uint IGameEvent<EventCsGameDisconnected>.GetHash() => 0xC1557D00u;
}
