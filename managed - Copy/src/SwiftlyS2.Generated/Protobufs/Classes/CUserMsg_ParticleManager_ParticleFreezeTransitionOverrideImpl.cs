
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_ParticleFreezeTransitionOverrideImpl : TypedProtobuf<CUserMsg_ParticleManager_ParticleFreezeTransitionOverride>, CUserMsg_ParticleManager_ParticleFreezeTransitionOverride
{
  public CUserMsg_ParticleManager_ParticleFreezeTransitionOverrideImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float FreezeTransitionOverride
  { get => Accessor.GetFloat("freeze_transition_override"); set => Accessor.SetFloat("freeze_transition_override", value); }

}
