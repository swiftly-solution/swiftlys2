
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleSetFrozenImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleSetFrozen>, CUserMsg_ParticleManager_UpdateParticleSetFrozen
{
  public CUserMsg_ParticleManager_UpdateParticleSetFrozenImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool SetFrozen
  { get => Accessor.GetBool("set_frozen"); set => Accessor.SetBool("set_frozen", value); }


  public float TransitionDuration
  { get => Accessor.GetFloat("transition_duration"); set => Accessor.SetFloat("transition_duration", value); }

}
