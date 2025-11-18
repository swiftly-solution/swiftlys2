
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageHapticsManagerEffectImpl : NetMessage<CUserMessageHapticsManagerEffect>, CUserMessageHapticsManagerEffect
{
  public CUserMessageHapticsManagerEffectImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int HandId
  { get => Accessor.GetInt32("hand_id"); set => Accessor.SetInt32("hand_id", value); }


  public uint EffectNameHashCode
  { get => Accessor.GetUInt32("effect_name_hash_code"); set => Accessor.SetUInt32("effect_name_hash_code", value); }


  public float EffectScale
  { get => Accessor.GetFloat("effect_scale"); set => Accessor.SetFloat("effect_scale", value); }

}
