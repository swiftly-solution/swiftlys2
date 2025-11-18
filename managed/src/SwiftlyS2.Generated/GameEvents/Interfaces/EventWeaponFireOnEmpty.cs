using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "weapon_fire_on_empty"
/// </summary>
public interface EventWeaponFireOnEmpty : IGameEvent<EventWeaponFireOnEmpty>
{

    static EventWeaponFireOnEmpty IGameEvent<EventWeaponFireOnEmpty>.Create( nint address ) => new EventWeaponFireOnEmptyImpl(address);

    static string IGameEvent<EventWeaponFireOnEmpty>.GetName() => "weapon_fire_on_empty";

    static uint IGameEvent<EventWeaponFireOnEmpty>.GetHash() => 0xB2954170u;
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

}
