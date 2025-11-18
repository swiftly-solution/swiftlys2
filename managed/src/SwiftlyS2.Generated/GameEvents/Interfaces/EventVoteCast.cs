using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vote_cast"
/// </summary>
public interface EventVoteCast : IGameEvent<EventVoteCast>
{

    static EventVoteCast IGameEvent<EventVoteCast>.Create( nint address ) => new EventVoteCastImpl(address);

    static string IGameEvent<EventVoteCast>.GetName() => "vote_cast";

    static uint IGameEvent<EventVoteCast>.GetHash() => 0xFDAD5FE5u;
    /// <summary>
    /// which option the player voted on
    /// <br/>
    /// type: byte
    /// </summary>
    public byte VoteOption { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short Team { get; set; }

    /// <summary>
    /// player who voted
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who voted
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who voted
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who voted
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

}
