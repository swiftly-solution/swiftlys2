using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_phase_changed"
/// </summary>
internal class EventGamePhaseChangedImpl : GameEvent<EventGamePhaseChanged>, EventGamePhaseChanged
{

    public EventGamePhaseChangedImpl( nint address ) : base(address)
    {
    }

    public short NewPhase { get => (short)Accessor.GetInt32("new_phase"); set => Accessor.SetInt32("new_phase", value); }
}
