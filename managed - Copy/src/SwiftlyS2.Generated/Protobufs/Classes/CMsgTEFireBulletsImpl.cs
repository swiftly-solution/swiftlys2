
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEFireBulletsImpl : NetMessage<CMsgTEFireBullets>, CMsgTEFireBullets
{
  public CMsgTEFireBulletsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public uint WeaponId
  { get => Accessor.GetUInt32("weapon_id"); set => Accessor.SetUInt32("weapon_id", value); }


  public uint Mode
  { get => Accessor.GetUInt32("mode"); set => Accessor.SetUInt32("mode", value); }


  public uint Seed
  { get => Accessor.GetUInt32("seed"); set => Accessor.SetUInt32("seed", value); }


  public uint Player
  { get => Accessor.GetUInt32("player"); set => Accessor.SetUInt32("player", value); }


  public float Inaccuracy
  { get => Accessor.GetFloat("inaccuracy"); set => Accessor.SetFloat("inaccuracy", value); }


  public float RecoilIndex
  { get => Accessor.GetFloat("recoil_index"); set => Accessor.SetFloat("recoil_index", value); }


  public float Spread
  { get => Accessor.GetFloat("spread"); set => Accessor.SetFloat("spread", value); }


  public int SoundType
  { get => Accessor.GetInt32("sound_type"); set => Accessor.SetInt32("sound_type", value); }


  public uint ItemDefIndex
  { get => Accessor.GetUInt32("item_def_index"); set => Accessor.SetUInt32("item_def_index", value); }


  public uint SoundDspEffect
  { get => Accessor.GetUInt32("sound_dsp_effect"); set => Accessor.SetUInt32("sound_dsp_effect", value); }


  public Vector EntOrigin
  { get => Accessor.GetVector("ent_origin"); set => Accessor.SetVector("ent_origin", value); }


  public uint NumBulletsRemaining
  { get => Accessor.GetUInt32("num_bullets_remaining"); set => Accessor.SetUInt32("num_bullets_remaining", value); }


  public uint AttackType
  { get => Accessor.GetUInt32("attack_type"); set => Accessor.SetUInt32("attack_type", value); }


  public bool PlayerInair
  { get => Accessor.GetBool("player_inair"); set => Accessor.SetBool("player_inair", value); }


  public bool PlayerScoped
  { get => Accessor.GetBool("player_scoped"); set => Accessor.SetBool("player_scoped", value); }


  public int Tick
  { get => Accessor.GetInt32("tick"); set => Accessor.SetInt32("tick", value); }


  public CMsgTEFireBullets_Extra Extra
  { get => new CMsgTEFireBullets_ExtraImpl(NativeNetMessages.GetNestedMessage(Address, "extra"), false); }

}
