using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "map_shutdown"
/// </summary>
public interface EventMapShutdown : IGameEvent<EventMapShutdown> {

  static EventMapShutdown IGameEvent<EventMapShutdown>.Create(nint address) => new EventMapShutdownImpl(address);

  static string IGameEvent<EventMapShutdown>.GetName() => "map_shutdown";

  static uint IGameEvent<EventMapShutdown>.GetHash() => 0xCA1507ECu;
}
