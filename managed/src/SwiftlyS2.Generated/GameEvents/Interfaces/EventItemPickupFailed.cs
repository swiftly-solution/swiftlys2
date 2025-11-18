using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_pickup_failed"
/// </summary>
public interface EventItemPickupFailed : IGameEvent<EventItemPickupFailed>
{

    static EventItemPickupFailed IGameEvent<EventItemPickupFailed>.Create( nint address ) => new EventItemPickupFailedImpl(address);

    static string IGameEvent<EventItemPickupFailed>.GetName() => "item_pickup_failed";

    static uint IGameEvent<EventItemPickupFailed>.GetHash() => 0x0F6D19A9u;
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
    /// type: string
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short Reason { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short Limit { get; set; }

}
