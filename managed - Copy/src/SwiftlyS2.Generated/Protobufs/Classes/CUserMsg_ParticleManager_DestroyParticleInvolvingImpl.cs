
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_DestroyParticleInvolvingImpl : TypedProtobuf<CUserMsg_ParticleManager_DestroyParticleInvolving>, CUserMsg_ParticleManager_DestroyParticleInvolving
{
  public CUserMsg_ParticleManager_DestroyParticleInvolvingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool DestroyImmediately
  { get => Accessor.GetBool("destroy_immediately"); set => Accessor.SetBool("destroy_immediately", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }

}
