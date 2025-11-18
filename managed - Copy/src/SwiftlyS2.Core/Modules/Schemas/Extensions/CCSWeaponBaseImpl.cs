using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CCSWeaponBaseImpl : CCSWeaponBase {

  public CCSWeaponBaseVData WeaponBaseVData { 
    get {
      return VData.As<CCSWeaponBaseVData>();
    }
  }
}