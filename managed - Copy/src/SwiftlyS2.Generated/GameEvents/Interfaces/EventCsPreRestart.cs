using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_pre_restart"
/// </summary>
public interface EventCsPreRestart : IGameEvent<EventCsPreRestart> {

  static EventCsPreRestart IGameEvent<EventCsPreRestart>.Create(nint address) => new EventCsPreRestartImpl(address);

  static string IGameEvent<EventCsPreRestart>.GetName() => "cs_pre_restart";

  static uint IGameEvent<EventCsPreRestart>.GetHash() => 0xE870A219u;
}
