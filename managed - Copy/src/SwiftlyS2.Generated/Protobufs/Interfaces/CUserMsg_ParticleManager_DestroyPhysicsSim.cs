
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_DestroyPhysicsSim : ITypedProtobuf<CUserMsg_ParticleManager_DestroyPhysicsSim>
{
  static CUserMsg_ParticleManager_DestroyPhysicsSim ITypedProtobuf<CUserMsg_ParticleManager_DestroyPhysicsSim>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_DestroyPhysicsSimImpl(handle, isManuallyAllocated);


}
