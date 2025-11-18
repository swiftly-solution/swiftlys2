using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "guardian_wave_restart"
/// </summary>
internal class EventGuardianWaveRestartImpl : GameEvent<EventGuardianWaveRestart>, EventGuardianWaveRestart
{

  public EventGuardianWaveRestartImpl(nint address) : base(address)
  {
  }
}
