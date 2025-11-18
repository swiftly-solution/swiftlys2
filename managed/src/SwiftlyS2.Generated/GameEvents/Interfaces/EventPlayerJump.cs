using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_jump"
/// </summary>
public interface EventPlayerJump : IGameEvent<EventPlayerJump>
{

    static EventPlayerJump IGameEvent<EventPlayerJump>.Create( nint address ) => new EventPlayerJumpImpl(address);

    static string IGameEvent<EventPlayerJump>.GetName() => "player_jump";

    static uint IGameEvent<EventPlayerJump>.GetHash() => 0xA8C90F75u;
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

}
