using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "local_player_pawn_changed"
/// </summary>
public interface EventLocalPlayerPawnChanged : IGameEvent<EventLocalPlayerPawnChanged> {

  static EventLocalPlayerPawnChanged IGameEvent<EventLocalPlayerPawnChanged>.Create(nint address) => new EventLocalPlayerPawnChangedImpl(address);

  static string IGameEvent<EventLocalPlayerPawnChanged>.GetName() => "local_player_pawn_changed";

  static uint IGameEvent<EventLocalPlayerPawnChanged>.GetHash() => 0x4703D4F0u;
}
