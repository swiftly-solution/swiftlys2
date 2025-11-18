using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_cast_yes"
/// </summary>
public interface EventVoteCastYes : IGameEvent<EventVoteCastYes>
{

    static EventVoteCastYes IGameEvent<EventVoteCastYes>.Create( nint address ) => new EventVoteCastYesImpl(address);

    static string IGameEvent<EventVoteCastYes>.GetName() => "vote_cast_yes";

    static uint IGameEvent<EventVoteCastYes>.GetHash() => 0xC6314219u;
    /// <summary>
    /// type: byte
    /// </summary>
    public byte Team { get; set; }

    /// <summary>
    /// entity id of the voter
    /// <br/>
    /// type: long
    /// </summary>
    public int EntityID { get; set; }

}
