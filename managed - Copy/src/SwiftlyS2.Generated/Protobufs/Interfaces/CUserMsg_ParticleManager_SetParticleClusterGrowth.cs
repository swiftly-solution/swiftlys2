
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleClusterGrowth : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleClusterGrowth>
{
  static CUserMsg_ParticleManager_SetParticleClusterGrowth ITypedProtobuf<CUserMsg_ParticleManager_SetParticleClusterGrowth>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleClusterGrowthImpl(handle, isManuallyAllocated);


  public float Duration { get; set; }


  public Vector Origin { get; set; }

}
