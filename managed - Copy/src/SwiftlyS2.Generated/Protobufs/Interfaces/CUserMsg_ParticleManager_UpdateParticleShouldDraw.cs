
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleShouldDraw : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleShouldDraw>
{
  static CUserMsg_ParticleManager_UpdateParticleShouldDraw ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleShouldDraw>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleShouldDrawImpl(handle, isManuallyAllocated);


  public bool ShouldDraw { get; set; }

}
