
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_DestroyParticleImpl : TypedProtobuf<CUserMsg_ParticleManager_DestroyParticle>, CUserMsg_ParticleManager_DestroyParticle
{
  public CUserMsg_ParticleManager_DestroyParticleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool DestroyImmediately
  { get => Accessor.GetBool("destroy_immediately"); set => Accessor.SetBool("destroy_immediately", value); }

}
