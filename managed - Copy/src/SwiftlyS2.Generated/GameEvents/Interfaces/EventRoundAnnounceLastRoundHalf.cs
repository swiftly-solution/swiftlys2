using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_announce_last_round_half"
/// </summary>
public interface EventRoundAnnounceLastRoundHalf : IGameEvent<EventRoundAnnounceLastRoundHalf> {

  static EventRoundAnnounceLastRoundHalf IGameEvent<EventRoundAnnounceLastRoundHalf>.Create(nint address) => new EventRoundAnnounceLastRoundHalfImpl(address);

  static string IGameEvent<EventRoundAnnounceLastRoundHalf>.GetName() => "round_announce_last_round_half";

  static uint IGameEvent<EventRoundAnnounceLastRoundHalf>.GetHash() => 0x3EF49D9Du;
}
