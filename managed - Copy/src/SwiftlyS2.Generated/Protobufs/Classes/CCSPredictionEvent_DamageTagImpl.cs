
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSPredictionEvent_DamageTagImpl : TypedProtobuf<CCSPredictionEvent_DamageTag>, CCSPredictionEvent_DamageTag
{
  public CCSPredictionEvent_DamageTagImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float FlinchModSmall
  { get => Accessor.GetFloat("flinch_mod_small"); set => Accessor.SetFloat("flinch_mod_small", value); }


  public float FlinchModLarge
  { get => Accessor.GetFloat("flinch_mod_large"); set => Accessor.SetFloat("flinch_mod_large", value); }


  public float FriendlyFireDamageReductionRatio
  { get => Accessor.GetFloat("friendly_fire_damage_reduction_ratio"); set => Accessor.SetFloat("friendly_fire_damage_reduction_ratio", value); }

}
