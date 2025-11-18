using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CPlayer_WeaponServicesImpl {

  public void DropWeapon(CBasePlayerWeapon weapon) {
    GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address);
  }

  public void RemoveWeapon(CBasePlayerWeapon weapon) {
    GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address);
    weapon.Despawn();
  }

  public void SelectWeapon(CBasePlayerWeapon weapon) {
    GameFunctions.CCSPlayer_WeaponServices_SelectWeapon(Address, weapon.Address);
  }

  public void DropWeaponBySlot( gear_slot_t slot ) {
    MyWeapons.ToList().ForEach(weapon => {
      if ( weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot ) {
        DropWeapon(weapon.Value);
      }
    });
  }

  public void RemoveWeaponBySlot( gear_slot_t slot ) {
    MyWeapons.ToList().ForEach(weapon => {
      if ( weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot ) {
        RemoveWeapon(weapon.Value);
      }
    });
  }

  public void SelectWeaponBySlot( gear_slot_t slot ) {
    MyWeapons.ToList().ForEach(weapon => {
      if ( weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot ) {
        SelectWeapon(weapon.Value);
        return;
      }
    });
  }

  public void DropWeaponByDesignerName( string designerName ) {
    MyWeapons.ToList().ForEach(weapon => {
      if ( weapon.Value?.Entity?.DesignerName == designerName ) {
        DropWeapon(weapon.Value);
      }
    });
  }

  public void RemoveWeaponByDesignerName( string designerName ) {
    MyWeapons.ToList().ForEach(weapon => {
      if ( weapon.Value?.Entity?.DesignerName == designerName ) {
        RemoveWeapon(weapon.Value);
      }
    });
  }

  public void SelectWeaponByDesignerName( string designerName ) {
    MyWeapons.ToList().ForEach(weapon => {
      if ( weapon.Value?.Entity?.DesignerName == designerName ) {
        SelectWeapon(weapon.Value);
      }
    });
  }

  public void DropWeaponByClass<T>() where T : CBasePlayerWeapon {
    var name = EntitySystemService.TypeToDesignerName[typeof(T)];
    DropWeaponByDesignerName(name);
  }

  public void RemoveWeaponByClass<T>() where T : CBasePlayerWeapon {
    var name = EntitySystemService.TypeToDesignerName[typeof(T)];
    RemoveWeaponByDesignerName(name);
  }

  public void SelectWeaponByClass<T>() where T : CBasePlayerWeapon {
    var name = EntitySystemService.TypeToDesignerName[typeof(T)];
    SelectWeaponByDesignerName(name);
  }

}