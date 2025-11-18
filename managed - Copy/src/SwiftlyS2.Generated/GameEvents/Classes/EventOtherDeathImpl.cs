using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "other_death"
/// </summary>
internal class EventOtherDeathImpl : GameEvent<EventOtherDeath>, EventOtherDeath
{

  public EventOtherDeathImpl(nint address) : base(address)
  {
  }

  // other entity ID who died
  public short OtherID
  { get => (short)Accessor.GetInt32("otherid"); set => Accessor.SetInt32("otherid", value); }

  // other entity type
  public string OtherType
  { get => Accessor.GetString("othertype"); set => Accessor.SetString("othertype", value); }

  // user ID who killed
  public short Attacker
  { get => (short)Accessor.GetInt32("attacker"); set => Accessor.SetInt32("attacker", value); }

  // weapon name killer used
  public string Weapon
  { get => Accessor.GetString("weapon"); set => Accessor.SetString("weapon", value); }

  // inventory item id of weapon killer used
  public string WeaponItemid
  { get => Accessor.GetString("weapon_itemid"); set => Accessor.SetString("weapon_itemid", value); }

  // faux item id of weapon killer used
  public string WeaponFauxitemid
  { get => Accessor.GetString("weapon_fauxitemid"); set => Accessor.SetString("weapon_fauxitemid", value); }

  public string WeaponOriginalownerXuid
  { get => Accessor.GetString("weapon_originalowner_xuid"); set => Accessor.SetString("weapon_originalowner_xuid", value); }

  // singals a headshot
  public bool Headshot
  { get => Accessor.GetBool("headshot"); set => Accessor.SetBool("headshot", value); }

  // number of objects shot penetrated before killing target
  public short Penetrated
  { get => (short)Accessor.GetInt32("penetrated"); set => Accessor.SetInt32("penetrated", value); }

  // kill happened without a scope, used for death notice icon
  public bool NoScope
  { get => Accessor.GetBool("noscope"); set => Accessor.SetBool("noscope", value); }

  // hitscan weapon went through smoke grenade
  public bool ThruSmoke
  { get => Accessor.GetBool("thrusmoke"); set => Accessor.SetBool("thrusmoke", value); }

  // attacker was blind from flashbang
  public bool AttackerBlind
  { get => Accessor.GetBool("attackerblind"); set => Accessor.SetBool("attackerblind", value); }
}
