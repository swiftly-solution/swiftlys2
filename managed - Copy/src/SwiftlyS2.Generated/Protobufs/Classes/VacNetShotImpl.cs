
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class VacNetShotImpl : TypedProtobuf<VacNetShot>, VacNetShot
{
  public VacNetShotImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong SteamidPlayer
  { get => Accessor.GetUInt64("steamid_player"); set => Accessor.SetUInt64("steamid_player", value); }


  public int RoundNumber
  { get => Accessor.GetInt32("round_number"); set => Accessor.SetInt32("round_number", value); }


  public int HitType
  { get => Accessor.GetInt32("hit_type"); set => Accessor.SetInt32("hit_type", value); }


  public int WeaponType
  { get => Accessor.GetInt32("weapon_type"); set => Accessor.SetInt32("weapon_type", value); }


  public float DistanceToHurtTarget
  { get => Accessor.GetFloat("distance_to_hurt_target"); set => Accessor.SetFloat("distance_to_hurt_target", value); }


  public IProtobufRepeatedFieldValueType<float> DeltaYawWindow
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "delta_yaw_window"); }


  public IProtobufRepeatedFieldValueType<float> DeltaPitchWindow
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "delta_pitch_window"); }

}
