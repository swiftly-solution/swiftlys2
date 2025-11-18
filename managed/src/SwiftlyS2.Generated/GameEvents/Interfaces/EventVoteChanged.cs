using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_changed"
/// </summary>
public interface EventVoteChanged : IGameEvent<EventVoteChanged>
{

    static EventVoteChanged IGameEvent<EventVoteChanged>.Create( nint address ) => new EventVoteChangedImpl(address);

    static string IGameEvent<EventVoteChanged>.GetName() => "vote_changed";

    static uint IGameEvent<EventVoteChanged>.GetHash() => 0xA69CF8EAu;
    /// <summary>
    /// type: byte
    /// </summary>
    public byte YesVotes { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte NoVotes { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte PotentialVotes { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte VoteOption1 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte VoteOption2 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte VoteOption3 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte VoteOption4 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte VoteOption5 { get; set; }

}
