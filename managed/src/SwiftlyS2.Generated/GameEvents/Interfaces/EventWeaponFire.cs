using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "weapon_fire"
/// </summary>
public interface EventWeaponFire : IGameEvent<EventWeaponFire>
{

    static EventWeaponFire IGameEvent<EventWeaponFire>.Create( nint address ) => new EventWeaponFireImpl(address);

    static string IGameEvent<EventWeaponFire>.GetName() => "weapon_fire";

    static uint IGameEvent<EventWeaponFire>.GetHash() => 0x78A2D0FEu;
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
    /// weapon name used
    /// <br/>
    /// type: string
    /// </summary>
    public string Weapon { get; set; }

    /// <summary>
    /// is weapon silenced
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Silenced { get; set; }

}
