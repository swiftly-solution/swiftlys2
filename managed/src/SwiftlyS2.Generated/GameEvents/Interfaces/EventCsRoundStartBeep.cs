using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_round_start_beep"
/// </summary>
public interface EventCsRoundStartBeep : IGameEvent<EventCsRoundStartBeep>
{

    static EventCsRoundStartBeep IGameEvent<EventCsRoundStartBeep>.Create( nint address ) => new EventCsRoundStartBeepImpl(address);

    static string IGameEvent<EventCsRoundStartBeep>.GetName() => "cs_round_start_beep";

    static uint IGameEvent<EventCsRoundStartBeep>.GetHash() => 0x4DB83630u;
}
