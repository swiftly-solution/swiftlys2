using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface IWeaponDrop
{
    /// <summary>
    /// The player who dropped the weapon.
    /// </summary>
    public IPlayer Player { get; set; }
    /// <summary>
    /// The weapon.
    /// </summary>
    public CBasePlayerWeapon? Weapon { get; }
    /// <summary>
    /// Swapping weapon with one from the ground.
    /// </summary>
    public bool SwappingWeapon { get; }
    /// <summary>
    /// The result of the hook.
    /// </summary>
    public HookResult Result { get; set; }
}

public delegate void OnWeaponDropDelegate( ref IWeaponDrop drop );

public interface IWeaponDropEvents
{
    /// <summary>
    /// Event triggered before a weapon is dropped.
    /// </summary>
    public event OnWeaponDropDelegate Pre;

    /// <summary>
    /// Event triggered after a weapon is dropped.
    /// </summary>
    public event OnWeaponDropDelegate Post;
}
