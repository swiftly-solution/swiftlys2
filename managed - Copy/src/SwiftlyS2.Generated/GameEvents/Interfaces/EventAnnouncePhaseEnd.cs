using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "announce_phase_end"
/// </summary>
public interface EventAnnouncePhaseEnd : IGameEvent<EventAnnouncePhaseEnd> {

  static EventAnnouncePhaseEnd IGameEvent<EventAnnouncePhaseEnd>.Create(nint address) => new EventAnnouncePhaseEndImpl(address);

  static string IGameEvent<EventAnnouncePhaseEnd>.GetName() => "announce_phase_end";

  static uint IGameEvent<EventAnnouncePhaseEnd>.GetHash() => 0x5063C41Cu;
}
