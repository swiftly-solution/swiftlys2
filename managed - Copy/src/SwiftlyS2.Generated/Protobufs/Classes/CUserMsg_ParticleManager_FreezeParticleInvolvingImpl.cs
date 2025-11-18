
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_FreezeParticleInvolvingImpl : TypedProtobuf<CUserMsg_ParticleManager_FreezeParticleInvolving>, CUserMsg_ParticleManager_FreezeParticleInvolving
{
  public CUserMsg_ParticleManager_FreezeParticleInvolvingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool SetFrozen
  { get => Accessor.GetBool("set_frozen"); set => Accessor.SetBool("set_frozen", value); }


  public float TransitionDuration
  { get => Accessor.GetFloat("transition_duration"); set => Accessor.SetFloat("transition_duration", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }

}
