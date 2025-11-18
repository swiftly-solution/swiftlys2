using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ammo_pickup"
/// </summary>
public interface EventAmmoPickup : IGameEvent<EventAmmoPickup>
{

    static EventAmmoPickup IGameEvent<EventAmmoPickup>.Create( nint address ) => new EventAmmoPickupImpl(address);

    static string IGameEvent<EventAmmoPickup>.GetName() => "ammo_pickup";

    static uint IGameEvent<EventAmmoPickup>.GetHash() => 0x374B5BCCu;
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
    /// the weapon entindex
    /// <br/>
    /// type: long
    /// </summary>
    public int Index { get; set; }

}
