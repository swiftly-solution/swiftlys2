using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnWeaponServicesCanUseHookEvent : IOnWeaponServicesCanUseHookEvent {
  public required CCSPlayer_WeaponServices WeaponServices { get; set; }
  public required CCSWeaponBase Weapon { get; set; }
  public required bool OriginalResult { get; set; }
  public bool Intercepted { get; set; } = false;

  public void SetResult(bool result) {
    OriginalResult = result;
    Intercepted = true;
  }
}