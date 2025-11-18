using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ammo_refill"
/// </summary>
public interface EventAmmoRefill : IGameEvent<EventAmmoRefill>
{

    static EventAmmoRefill IGameEvent<EventAmmoRefill>.Create( nint address ) => new EventAmmoRefillImpl(address);

    static string IGameEvent<EventAmmoRefill>.GetName() => "ammo_refill";

    static uint IGameEvent<EventAmmoRefill>.GetHash() => 0x65179124u;
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
    /// type: bool
    /// </summary>
    public bool Success { get; set; }

}
