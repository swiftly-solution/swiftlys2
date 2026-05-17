using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICanAcquireItem
{
    /// <summary>
    /// The player who dropped the weapon.
    /// </summary>
    public IPlayer Player { get; set; }
    /// <summary>
    /// The econ item view.
    /// </summary>
    public CEconItemView EconItemView { get; }

    /// <summary>
    /// The weapon vdata if found, otherwise null.
    /// </summary>
    public CCSWeaponBaseVData? WeaponVData { get; }

    /// <summary>
    /// The acquire method.
    /// </summary>
    public AcquireMethod AcquireMethod { get; }

    /// <summary>
    /// The original result of the CanAcquire call.
    /// </summary>
    public AcquireResult OriginalResult { get; }

    /// <summary>
    /// Intercept and modify the acquire result.
    /// This will modify the acquire result and stop the following hooks and original function.
    /// </summary>
    /// <param name="result">The result to modify.</param>
    public void SetAcquireResult( AcquireResult result );

    /// <summary>
    /// If the event has been intercepted (the result has been changed).
    /// </summary>
    public bool Intercepted { get; set; }
}

public delegate void OnCanAcquireItemDelegate( ref ICanAcquireItem canAcquire );

public interface ICanAcquireItemEvents
{
    /// <summary>
    /// Event triggered before an item can be acquired.
    /// </summary>
    public event OnCanAcquireItemDelegate Pre;

    /// <summary>
    /// Event triggered after an item can be acquired.
    /// </summary>
    public event OnCanAcquireItemDelegate Post;
}
