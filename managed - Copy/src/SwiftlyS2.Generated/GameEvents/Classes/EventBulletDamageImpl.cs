using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bullet_damage"
/// </summary>
internal class EventBulletDamageImpl : GameEvent<EventBulletDamage>, EventBulletDamage
{

  public EventBulletDamageImpl(nint address) : base(address)
  {
  }

  // player index who was hurt
  public int Victim
  { get => Accessor.GetPlayerSlot("victim"); set => Accessor.SetPlayerSlot("victim", value); }

  // player index who attacked
  public int Attacker
  { get => Accessor.GetPlayerSlot("attacker"); set => Accessor.SetPlayerSlot("attacker", value); }

  // how far the bullet travelled before it hit the player
  public float Distance
  { get => Accessor.GetFloat("distance"); set => Accessor.SetFloat("distance", value); }

  // direction vector of the bullet
  public float DamageDirX
  { get => Accessor.GetFloat("damage_dir_x"); set => Accessor.SetFloat("damage_dir_x", value); }

  // direction vector of the bullet
  public float DamageDirY
  { get => Accessor.GetFloat("damage_dir_y"); set => Accessor.SetFloat("damage_dir_y", value); }

  // direction vector of the bullet
  public float DamageDirZ
  { get => Accessor.GetFloat("damage_dir_z"); set => Accessor.SetFloat("damage_dir_z", value); }

  // how many surfaces were penetrated
  public byte NumPenetrations
  { get => (byte)Accessor.GetInt32("num_penetrations"); set => Accessor.SetInt32("num_penetrations", value); }

  // was the shooter noscoped?
  public bool NoScope
  { get => Accessor.GetBool("no_scope"); set => Accessor.SetBool("no_scope", value); }

  // was the shooter jumping?
  public bool InAir
  { get => Accessor.GetBool("in_air"); set => Accessor.SetBool("in_air", value); }

  // shoot angle x
  public float ShootAngX
  { get => Accessor.GetFloat("shoot_ang_x"); set => Accessor.SetFloat("shoot_ang_x", value); }

  // shoot angle y
  public float ShootAngY
  { get => Accessor.GetFloat("shoot_ang_y"); set => Accessor.SetFloat("shoot_ang_y", value); }

  // shoot angle z
  public float ShootAngZ
  { get => Accessor.GetFloat("shoot_ang_z"); set => Accessor.SetFloat("shoot_ang_z", value); }

  // aim punch x
  public float AimPunchX
  { get => Accessor.GetFloat("aim_punch_x"); set => Accessor.SetFloat("aim_punch_x", value); }

  // aim punch y
  public float AimPunchY
  { get => Accessor.GetFloat("aim_punch_y"); set => Accessor.SetFloat("aim_punch_y", value); }

  // aim punch z
  public float AimPunchZ
  { get => Accessor.GetFloat("aim_punch_z"); set => Accessor.SetFloat("aim_punch_z", value); }

  // attack tick
  public int AttackTickCount
  { get => Accessor.GetInt32("attack_tick_count"); set => Accessor.SetInt32("attack_tick_count", value); }

  // attack frac
  public float AttackTickFrac
  { get => Accessor.GetFloat("attack_tick_frac"); set => Accessor.SetFloat("attack_tick_frac", value); }

  // render tick
  public int RenderTickCount
  { get => Accessor.GetInt32("render_tick_count"); set => Accessor.SetInt32("render_tick_count", value); }

  // render frac
  public float RenderTickFrac
  { get => Accessor.GetFloat("render_tick_frac"); set => Accessor.SetFloat("render_tick_frac", value); }

  // total inaccuracy
  public float InaccuracyTotal
  { get => Accessor.GetFloat("inaccuracy_total"); set => Accessor.SetFloat("inaccuracy_total", value); }

  // move inaccuracy
  public float InaccuracyMove
  { get => Accessor.GetFloat("inaccuracy_move"); set => Accessor.SetFloat("inaccuracy_move", value); }

  // air inaccuracy
  public float InaccuracyAir
  { get => Accessor.GetFloat("inaccuracy_air"); set => Accessor.SetFloat("inaccuracy_air", value); }

  // recoil index. Yes this is really a float.
  public float RecoilIndex
  { get => Accessor.GetFloat("recoil_index"); set => Accessor.SetFloat("recoil_index", value); }

  // lag compensation type
  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }
}
