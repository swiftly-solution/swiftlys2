using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_pickup_slerp"
/// </summary>
public interface EventItemPickupSlerp : IGameEvent<EventItemPickupSlerp>
{

    static EventItemPickupSlerp IGameEvent<EventItemPickupSlerp>.Create( nint address ) => new EventItemPickupSlerpImpl(address);

    static string IGameEvent<EventItemPickupSlerp>.GetName() => "item_pickup_slerp";

    static uint IGameEvent<EventItemPickupSlerp>.GetHash() => 0x88B06F48u;
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
    /// type: short
    /// </summary>
    public short Index { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short Behavior { get; set; }

}
