using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "match_end_conditions"
/// </summary>
public interface EventMatchEndConditions : IGameEvent<EventMatchEndConditions>
{

    static EventMatchEndConditions IGameEvent<EventMatchEndConditions>.Create( nint address ) => new EventMatchEndConditionsImpl(address);

    static string IGameEvent<EventMatchEndConditions>.GetName() => "match_end_conditions";

    static uint IGameEvent<EventMatchEndConditions>.GetHash() => 0x036AAC37u;
    /// <summary>
    /// type: long
    /// </summary>
    public int FragS { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int MaxRounds { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int WinRounds { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int Time { get; set; }

}
