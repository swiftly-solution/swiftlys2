using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "survival_announce_phase"
/// </summary>
internal class EventSurvivalAnnouncePhaseImpl : GameEvent<EventSurvivalAnnouncePhase>, EventSurvivalAnnouncePhase
{

  public EventSurvivalAnnouncePhaseImpl(nint address) : base(address)
  {
  }

  // The phase #
  public short Phase
  { get => (short)Accessor.GetInt32("phase"); set => Accessor.SetInt32("phase", value); }
}
