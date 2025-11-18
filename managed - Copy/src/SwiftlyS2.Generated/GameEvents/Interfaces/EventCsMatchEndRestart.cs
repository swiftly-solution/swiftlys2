using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_match_end_restart"
/// </summary>
public interface EventCsMatchEndRestart : IGameEvent<EventCsMatchEndRestart> {

  static EventCsMatchEndRestart IGameEvent<EventCsMatchEndRestart>.Create(nint address) => new EventCsMatchEndRestartImpl(address);

  static string IGameEvent<EventCsMatchEndRestart>.GetName() => "cs_match_end_restart";

  static uint IGameEvent<EventCsMatchEndRestart>.GetHash() => 0xFB2BFA6Fu;
}
