using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "door_moving"
/// </summary>
public interface EventDoorMoving : IGameEvent<EventDoorMoving>
{

    static EventDoorMoving IGameEvent<EventDoorMoving>.Create( nint address ) => new EventDoorMovingImpl(address);

    static string IGameEvent<EventDoorMoving>.GetName() => "door_moving";

    static uint IGameEvent<EventDoorMoving>.GetHash() => 0x253FA168u;
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int EntIndex { get; set; }

}
