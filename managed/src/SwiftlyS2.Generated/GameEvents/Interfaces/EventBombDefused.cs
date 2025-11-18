using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_defused"
/// </summary>
public interface EventBombDefused : IGameEvent<EventBombDefused>
{

    static EventBombDefused IGameEvent<EventBombDefused>.Create( nint address ) => new EventBombDefusedImpl(address);

    static string IGameEvent<EventBombDefused>.GetName() => "bomb_defused";

    static uint IGameEvent<EventBombDefused>.GetHash() => 0xD4FCB0A4u;
    /// <summary>
    /// player who defused the bomb
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who defused the bomb
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who defused the bomb
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who defused the bomb
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// bombsite index
    /// <br/>
    /// type: short
    /// </summary>
    public short Site { get; set; }

}
