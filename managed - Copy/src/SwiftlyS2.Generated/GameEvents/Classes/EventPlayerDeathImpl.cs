using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_death"
/// a game event, name may be 32 charaters long
/// </summary>
internal class EventPlayerDeathImpl : GameEvent<EventPlayerDeath>, EventPlayerDeath
{

  public EventPlayerDeathImpl(nint address) : base(address)
  {
  }

  // user ID who died
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // user ID who died
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // user ID who died
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // user ID who died
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // user ID who killed
  public int Attacker
  { get => Accessor.GetPlayerSlot("attacker"); set => Accessor.SetPlayerSlot("attacker", value); }

  // player who assisted in the kill
  public int Assister
  { get => Accessor.GetPlayerSlot("assister"); set => Accessor.SetPlayerSlot("assister", value); }

  // assister helped with a flash
  public bool AssistedFlash
  { get => Accessor.GetBool("assistedflash"); set => Accessor.SetBool("assistedflash", value); }

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

  // did killer dominate victim with this kill
  public short Dominated
  { get => (short)Accessor.GetInt32("dominated"); set => Accessor.SetInt32("dominated", value); }

  // did killer get revenge on victim with this kill
  public short Revenge
  { get => (short)Accessor.GetInt32("revenge"); set => Accessor.SetInt32("revenge", value); }

  // is the kill resulting in squad wipe
  public short Wipe
  { get => (short)Accessor.GetInt32("wipe"); set => Accessor.SetInt32("wipe", value); }

  // number of objects shot penetrated before killing target
  public short Penetrated
  { get => (short)Accessor.GetInt32("penetrated"); set => Accessor.SetInt32("penetrated", value); }

  // if replay data is unavailable, this will be present and set to false
  public bool NoReplay
  { get => Accessor.GetBool("noreplay"); set => Accessor.SetBool("noreplay", value); }

  // kill happened without a scope, used for death notice icon
  public bool NoScope
  { get => Accessor.GetBool("noscope"); set => Accessor.SetBool("noscope", value); }

  // hitscan weapon went through smoke grenade
  public bool ThruSmoke
  { get => Accessor.GetBool("thrusmoke"); set => Accessor.SetBool("thrusmoke", value); }

  // attacker was blind from flashbang
  public bool AttackerBlind
  { get => Accessor.GetBool("attackerblind"); set => Accessor.SetBool("attackerblind", value); }

  // distance to victim in meters
  public float Distance
  { get => Accessor.GetFloat("distance"); set => Accessor.SetFloat("distance", value); }

  // damage done to health
  public short DmgHealth
  { get => (short)Accessor.GetInt32("dmg_health"); set => Accessor.SetInt32("dmg_health", value); }

  // damage done to armor
  public byte DmgArmor
  { get => (byte)Accessor.GetInt32("dmg_armor"); set => Accessor.SetInt32("dmg_armor", value); }

  // hitgroup that was damaged
  public byte HitGroup
  { get => (byte)Accessor.GetInt32("hitgroup"); set => Accessor.SetInt32("hitgroup", value); }

  // attacker was in midair
  public bool AttackerInAir
  { get => Accessor.GetBool("attackerinair"); set => Accessor.SetBool("attackerinair", value); }
}
