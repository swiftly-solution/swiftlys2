using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "start_vote"
/// </summary>
public interface EventStartVote : IGameEvent<EventStartVote>
{

    static EventStartVote IGameEvent<EventStartVote>.Create( nint address ) => new EventStartVoteImpl(address);

    static string IGameEvent<EventStartVote>.GetName() => "start_vote";

    static uint IGameEvent<EventStartVote>.GetHash() => 0x637C08B4u;
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Type { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short VoteParameter { get; set; }

}
