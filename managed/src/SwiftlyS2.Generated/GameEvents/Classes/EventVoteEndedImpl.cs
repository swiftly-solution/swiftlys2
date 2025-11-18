using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vote_ended"
/// </summary>
internal class EventVoteEndedImpl : GameEvent<EventVoteEnded>, EventVoteEnded
{

    public EventVoteEndedImpl( nint address ) : base(address)
    {
    }
}
