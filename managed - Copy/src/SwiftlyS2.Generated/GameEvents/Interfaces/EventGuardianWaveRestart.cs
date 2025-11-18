using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "guardian_wave_restart"
/// </summary>
public interface EventGuardianWaveRestart : IGameEvent<EventGuardianWaveRestart> {

  static EventGuardianWaveRestart IGameEvent<EventGuardianWaveRestart>.Create(nint address) => new EventGuardianWaveRestartImpl(address);

  static string IGameEvent<EventGuardianWaveRestart>.GetName() => "guardian_wave_restart";

  static uint IGameEvent<EventGuardianWaveRestart>.GetHash() => 0xA7FB81A6u;
}
