using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_planted"
/// </summary>
public interface EventBombPlanted : IGameEvent<EventBombPlanted>
{

    static EventBombPlanted IGameEvent<EventBombPlanted>.Create( nint address ) => new EventBombPlantedImpl(address);

    static string IGameEvent<EventBombPlanted>.GetName() => "bomb_planted";

    static uint IGameEvent<EventBombPlanted>.GetHash() => 0x4E704C3Eu;
    /// <summary>
    /// player who planted the bomb
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who planted the bomb
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who planted the bomb
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who planted the bomb
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
