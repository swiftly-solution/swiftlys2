using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "game_phase_changed"
/// </summary>
public interface EventGamePhaseChanged : IGameEvent<EventGamePhaseChanged>
{

    static EventGamePhaseChanged IGameEvent<EventGamePhaseChanged>.Create( nint address ) => new EventGamePhaseChangedImpl(address);

    static string IGameEvent<EventGamePhaseChanged>.GetName() => "game_phase_changed";

    static uint IGameEvent<EventGamePhaseChanged>.GetHash() => 0x9FBE9554u;
    /// <summary>
    /// type: short
    /// </summary>
    public short NewPhase { get; set; }

}
