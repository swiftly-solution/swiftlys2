using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_started"
/// </summary>
public interface EventVoteStarted : IGameEvent<EventVoteStarted>
{

    static EventVoteStarted IGameEvent<EventVoteStarted>.Create( nint address ) => new EventVoteStartedImpl(address);

    static string IGameEvent<EventVoteStarted>.GetName() => "vote_started";

    static uint IGameEvent<EventVoteStarted>.GetHash() => 0xE0DFF70Fu;
    /// <summary>
    /// type: string
    /// </summary>
    public string Issue { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string Param1 { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string VoteData { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Team { get; set; }

    /// <summary>
    /// entity id of the player who initiated the vote
    /// <br/>
    /// type: long
    /// </summary>
    public int Initiator { get; set; }

}
