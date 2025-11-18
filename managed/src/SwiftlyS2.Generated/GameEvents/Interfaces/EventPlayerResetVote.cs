using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_reset_vote"
/// </summary>
public interface EventPlayerResetVote : IGameEvent<EventPlayerResetVote>
{

    static EventPlayerResetVote IGameEvent<EventPlayerResetVote>.Create( nint address ) => new EventPlayerResetVoteImpl(address);

    static string IGameEvent<EventPlayerResetVote>.GetName() => "player_reset_vote";

    static uint IGameEvent<EventPlayerResetVote>.GetHash() => 0x86ED6215u;
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
    /// type: bool
    /// </summary>
    public bool Vote { get; set; }

}
