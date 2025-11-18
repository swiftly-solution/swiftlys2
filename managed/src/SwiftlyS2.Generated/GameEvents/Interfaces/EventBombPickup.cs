using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_pickup"
/// </summary>
public interface EventBombPickup : IGameEvent<EventBombPickup>
{

    static EventBombPickup IGameEvent<EventBombPickup>.Create( nint address ) => new EventBombPickupImpl(address);

    static string IGameEvent<EventBombPickup>.GetName() => "bomb_pickup";

    static uint IGameEvent<EventBombPickup>.GetHash() => 0x97693BEEu;
    /// <summary>
    /// player pawn who picked up the bomb
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player pawn who picked up the bomb
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player pawn who picked up the bomb
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player pawn who picked up the bomb
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public int UserId { get; set; }

}
