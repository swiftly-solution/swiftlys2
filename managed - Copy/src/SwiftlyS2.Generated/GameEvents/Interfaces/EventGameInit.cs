using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "game_init"
/// sent when a new game is started
/// </summary>
public interface EventGameInit : IGameEvent<EventGameInit> {

  static EventGameInit IGameEvent<EventGameInit>.Create(nint address) => new EventGameInitImpl(address);

  static string IGameEvent<EventGameInit>.GetName() => "game_init";

  static uint IGameEvent<EventGameInit>.GetHash() => 0xF1BFEF5Au;
}
