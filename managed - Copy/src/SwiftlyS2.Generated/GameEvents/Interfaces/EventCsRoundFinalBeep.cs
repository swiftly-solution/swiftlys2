using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_round_final_beep"
/// </summary>
public interface EventCsRoundFinalBeep : IGameEvent<EventCsRoundFinalBeep> {

  static EventCsRoundFinalBeep IGameEvent<EventCsRoundFinalBeep>.Create(nint address) => new EventCsRoundFinalBeepImpl(address);

  static string IGameEvent<EventCsRoundFinalBeep>.GetName() => "cs_round_final_beep";

  static uint IGameEvent<EventCsRoundFinalBeep>.GetHash() => 0x75979130u;
}
