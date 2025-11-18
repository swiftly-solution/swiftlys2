using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "dm_bonus_weapon_start"
/// </summary>
public interface EventDmBonusWeaponStart : IGameEvent<EventDmBonusWeaponStart>
{

    static EventDmBonusWeaponStart IGameEvent<EventDmBonusWeaponStart>.Create( nint address ) => new EventDmBonusWeaponStartImpl(address);

    static string IGameEvent<EventDmBonusWeaponStart>.GetName() => "dm_bonus_weapon_start";

    static uint IGameEvent<EventDmBonusWeaponStart>.GetHash() => 0xB2B896A4u;
    /// <summary>
    /// The length of time that this bonus lasts
    /// <br/>
    /// type: short
    /// </summary>
    public short Time { get; set; }

    /// <summary>
    /// Loadout position of the bonus weapon
    /// <br/>
    /// type: short
    /// </summary>
    public short Pos { get; set; }

}
