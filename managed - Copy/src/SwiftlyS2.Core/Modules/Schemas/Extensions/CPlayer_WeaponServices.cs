namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CPlayer_WeaponServices
{

  /// <summary>
  /// Drop a weapon.
  /// </summary>
  /// <param name="weapon">The weapon to drop.</param>
  public void DropWeapon( CBasePlayerWeapon weapon );

  /// <summary>
  /// Drop and remove a weapon.
  /// </summary>
  /// <param name="weapon">The weapon to remove.</param>
  public void RemoveWeapon( CBasePlayerWeapon weapon );

  /// <summary>
  /// Make player select a weapon.
  /// </summary>
  /// <param name="weapon">The weapon to select.</param>
  public void SelectWeapon( CBasePlayerWeapon weapon );

  /// <summary>
  /// Drop a weapon by slot.
  /// </summary>
  /// <param name="slot">The slot to drop the weapon from.</param>
  public void DropWeaponBySlot( gear_slot_t slot );

  /// <summary>
  /// Remove a weapon by slot.
  /// </summary>
  /// <param name="slot">The slot to remove the weapon from.</param>
  public void RemoveWeaponBySlot( gear_slot_t slot );

  /// <summary>
  /// Select a weapon by slot.
  /// </summary>
  /// <param name="slot">The slot to select the weapon from.</param>
  public void SelectWeaponBySlot( gear_slot_t slot );

  /// <summary>
  /// Drop a weapon by designer name.
  /// </summary>
  /// <param name="designerName">The designer name of the weapon to drop.</param>
  public void DropWeaponByDesignerName( string designerName );

  /// <summary>
  /// Remove a weapon by designer name.
  /// </summary>
  /// <param name="designerName">The designer name of the weapon to remove.</param>
  public void RemoveWeaponByDesignerName( string designerName );

  /// <summary>
  /// Select a weapon by designer name.
  /// </summary>
  /// <param name="designerName">The designer name of the weapon to select.</param>
  public void SelectWeaponByDesignerName( string designerName );

  /// <summary>
  /// Drop all weapons with the specified class.
  /// </summary>
  /// <typeparam name="T">The weapon class.</typeparam>
  public void DropWeaponByClass<T>() where T : CBasePlayerWeapon;

  /// <summary>
  /// Drop and remove all weapons with the specified class.
  /// </summary>
  /// <typeparam name="T">The weapon class.</typeparam>
  public void RemoveWeaponByClass<T>() where T : CBasePlayerWeapon;

  /// <summary>
  /// Select a weapon by class.
  /// </summary>
  /// <typeparam name="T">The weapon class.</typeparam>
  public void SelectWeaponByClass<T>() where T : CBasePlayerWeapon;
}