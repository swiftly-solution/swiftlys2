using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_announce_final"
/// </summary>
public interface EventRoundAnnounceFinal : IGameEvent<EventRoundAnnounceFinal> {

  static EventRoundAnnounceFinal IGameEvent<EventRoundAnnounceFinal>.Create(nint address) => new EventRoundAnnounceFinalImpl(address);

  static string IGameEvent<EventRoundAnnounceFinal>.GetName() => "round_announce_final";

  static uint IGameEvent<EventRoundAnnounceFinal>.GetHash() => 0x6413CAF8u;
}
