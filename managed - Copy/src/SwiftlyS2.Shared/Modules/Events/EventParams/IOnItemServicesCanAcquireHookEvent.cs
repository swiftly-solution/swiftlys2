using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

public interface IOnItemServicesCanAcquireHookEvent {

  /// <summary>
  /// The item services.
  /// </summary>
  public CCSPlayer_ItemServices ItemServices { get; }

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
  public void SetAcquireResult(AcquireResult result);
}