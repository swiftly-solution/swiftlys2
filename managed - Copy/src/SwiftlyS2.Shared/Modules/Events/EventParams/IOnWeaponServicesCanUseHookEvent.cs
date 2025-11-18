using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

public interface IOnWeaponServicesCanUseHookEvent {
  
  /// <summary>
  /// The weapon services.
  /// </summary>
  public CCSPlayer_WeaponServices WeaponServices { get; }
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
}