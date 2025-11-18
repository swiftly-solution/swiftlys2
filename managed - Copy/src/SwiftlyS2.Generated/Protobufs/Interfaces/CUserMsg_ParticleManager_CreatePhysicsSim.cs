
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_CreatePhysicsSim : ITypedProtobuf<CUserMsg_ParticleManager_CreatePhysicsSim>
{
  static CUserMsg_ParticleManager_CreatePhysicsSim ITypedProtobuf<CUserMsg_ParticleManager_CreatePhysicsSim>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_CreatePhysicsSimImpl(handle, isManuallyAllocated);


  public string PropGroupName { get; set; }


  public bool UseHighQualitySimulation { get; set; }


  public uint MaxParticleCount { get; set; }

}
