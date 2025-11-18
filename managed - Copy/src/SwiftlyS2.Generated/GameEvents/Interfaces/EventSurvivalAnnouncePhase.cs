using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "survival_announce_phase"
/// </summary>
public interface EventSurvivalAnnouncePhase : IGameEvent<EventSurvivalAnnouncePhase> {

  static EventSurvivalAnnouncePhase IGameEvent<EventSurvivalAnnouncePhase>.Create(nint address) => new EventSurvivalAnnouncePhaseImpl(address);

  static string IGameEvent<EventSurvivalAnnouncePhase>.GetName() => "survival_announce_phase";

  static uint IGameEvent<EventSurvivalAnnouncePhase>.GetHash() => 0x70EE830Bu;
  /// <summary>
  /// The phase #
  /// <br/>
  /// type: short
  /// </summary>
  short Phase { get; set; }

}
