using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

[Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks instead.")]
public interface IOnWeaponServicesDropWeaponHook
{

    /// <summary>
    /// The weapon services.
    /// </summary>
    public CCSPlayer_WeaponServices WeaponServices { get; }
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