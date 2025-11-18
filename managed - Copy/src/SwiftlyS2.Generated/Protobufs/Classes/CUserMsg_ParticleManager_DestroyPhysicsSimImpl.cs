
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_DestroyPhysicsSimImpl : TypedProtobuf<CUserMsg_ParticleManager_DestroyPhysicsSim>, CUserMsg_ParticleManager_DestroyPhysicsSim
{
  public CUserMsg_ParticleManager_DestroyPhysicsSimImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


}
