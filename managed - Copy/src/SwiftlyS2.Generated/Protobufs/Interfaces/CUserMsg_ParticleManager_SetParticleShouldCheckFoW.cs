
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleShouldCheckFoW : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleShouldCheckFoW>
{
  static CUserMsg_ParticleManager_SetParticleShouldCheckFoW ITypedProtobuf<CUserMsg_ParticleManager_SetParticleShouldCheckFoW>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleShouldCheckFoWImpl(handle, isManuallyAllocated);


  public bool CheckFow { get; set; }

}
