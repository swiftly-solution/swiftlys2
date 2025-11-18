using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "dm_bonus_weapon_start"
/// </summary>
internal class EventDmBonusWeaponStartImpl : GameEvent<EventDmBonusWeaponStart>, EventDmBonusWeaponStart
{

  public EventDmBonusWeaponStartImpl(nint address) : base(address)
  {
  }

  // The length of time that this bonus lasts
  public short Time
  { get => (short)Accessor.GetInt32("time"); set => Accessor.SetInt32("time", value); }

  // Loadout position of the bonus weapon
  public short Pos
  { get => (short)Accessor.GetInt32("Pos"); set => Accessor.SetInt32("Pos", value); }
}
