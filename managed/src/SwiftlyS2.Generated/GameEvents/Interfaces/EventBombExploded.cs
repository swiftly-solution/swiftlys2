using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_exploded"
/// </summary>
public interface EventBombExploded : IGameEvent<EventBombExploded>
{

    static EventBombExploded IGameEvent<EventBombExploded>.Create( nint address ) => new EventBombExplodedImpl(address);

    static string IGameEvent<EventBombExploded>.GetName() => "bomb_exploded";

    static uint IGameEvent<EventBombExploded>.GetHash() => 0x9C543261u;
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
