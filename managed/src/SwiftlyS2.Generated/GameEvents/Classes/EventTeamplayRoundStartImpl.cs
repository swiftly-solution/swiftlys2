using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "teamplay_round_start"
/// round restart
/// </summary>
internal class EventTeamplayRoundStartImpl : GameEvent<EventTeamplayRoundStart>, EventTeamplayRoundStart
{

    public EventTeamplayRoundStartImpl( nint address ) : base(address)
    {
    }

    // is this a full reset of the map
    public bool FullReset { get => Accessor.GetBool("full_reset"); set => Accessor.SetBool("full_reset", value); }
}
