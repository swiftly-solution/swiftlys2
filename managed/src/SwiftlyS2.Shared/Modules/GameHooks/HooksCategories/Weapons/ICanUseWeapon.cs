using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICanUseWeapon
{
    /// <summary>
    /// The player who dropped the weapon.
    /// </summary>
    public IPlayer Player { get; set; }
    /// <summary>
    /// The weapon.
    /// </summary>
    public CCSWeaponBase Weapon { get; }
    /// <summary>
    /// The original result of the CanUse call.
    /// </summary>
    public bool OriginalResult { get; }

    /// <summary>
    /// Intercept and modify the can use result.
    /// This will modify the can use result and stop the following hooks and original function.
    /// </summary>
    /// <param name="result">The result to modify.</param>
    public void SetResult(bool result);

    /// <summary>
    /// Gets or sets a value indicating whether the can use call has been intercepted.
    /// </summary>
    public bool Intercepted { get; set; }
}

public delegate void OnCanUseWeaponDelegate(ref ICanUseWeapon canUse);

public interface ICanUseWeaponEvents
{
    /// <summary>
    /// Event triggered before a weapon can use logic is ran by game.
    /// </summary>
    public event OnCanUseWeaponDelegate Pre;

    /// <summary>
    /// Event triggered after the weapon can use logic is ran by game.
    /// </summary>
    public event OnCanUseWeaponDelegate Post;
}
