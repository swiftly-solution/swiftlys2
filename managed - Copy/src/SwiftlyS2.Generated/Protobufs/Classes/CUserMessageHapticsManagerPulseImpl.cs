
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageHapticsManagerPulseImpl : NetMessage<CUserMessageHapticsManagerPulse>, CUserMessageHapticsManagerPulse
{
  public CUserMessageHapticsManagerPulseImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int HandId
  { get => Accessor.GetInt32("hand_id"); set => Accessor.SetInt32("hand_id", value); }


  public float EffectAmplitude
  { get => Accessor.GetFloat("effect_amplitude"); set => Accessor.SetFloat("effect_amplitude", value); }


  public float EffectFrequency
  { get => Accessor.GetFloat("effect_frequency"); set => Accessor.SetFloat("effect_frequency", value); }


  public float EffectDuration
  { get => Accessor.GetFloat("effect_duration"); set => Accessor.SetFloat("effect_duration", value); }

}
