
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_ReleaseParticleIndexImpl : TypedProtobuf<CUserMsg_ParticleManager_ReleaseParticleIndex>, CUserMsg_ParticleManager_ReleaseParticleIndex
{
  public CUserMsg_ParticleManager_ReleaseParticleIndexImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


}
