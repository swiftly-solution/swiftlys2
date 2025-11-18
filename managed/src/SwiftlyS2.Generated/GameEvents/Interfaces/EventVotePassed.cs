using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_passed"
/// </summary>
public interface EventVotePassed : IGameEvent<EventVotePassed>
{

    static EventVotePassed IGameEvent<EventVotePassed>.Create( nint address ) => new EventVotePassedImpl(address);

    static string IGameEvent<EventVotePassed>.GetName() => "vote_passed";

    static uint IGameEvent<EventVotePassed>.GetHash() => 0x9B90008Eu;
    /// <summary>
    /// type: string
    /// </summary>
    public string Details { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string Param1 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Team { get; set; }

}
