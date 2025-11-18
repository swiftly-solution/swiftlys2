using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_pickup"
/// </summary>
public interface EventItemPickup : IGameEvent<EventItemPickup>
{

    static EventItemPickup IGameEvent<EventItemPickup>.Create( nint address ) => new EventItemPickupImpl(address);

    static string IGameEvent<EventItemPickup>.GetName() => "item_pickup";

    static uint IGameEvent<EventItemPickup>.GetHash() => 0x58CEF8C3u;
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
    /// either a weapon such as 'tmp' or 'hegrenade', or an item such as 'nvgs'
    /// <br/>
    /// type: string
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool Silent { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int DefIndex { get; set; }

}
