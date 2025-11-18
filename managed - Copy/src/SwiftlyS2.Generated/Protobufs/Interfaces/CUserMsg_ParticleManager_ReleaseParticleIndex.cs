
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_ReleaseParticleIndex : ITypedProtobuf<CUserMsg_ParticleManager_ReleaseParticleIndex>
{
  static CUserMsg_ParticleManager_ReleaseParticleIndex ITypedProtobuf<CUserMsg_ParticleManager_ReleaseParticleIndex>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_ReleaseParticleIndexImpl(handle, isManuallyAllocated);


}
