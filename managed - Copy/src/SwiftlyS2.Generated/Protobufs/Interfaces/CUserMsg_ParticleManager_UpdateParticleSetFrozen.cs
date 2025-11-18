
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleSetFrozen : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleSetFrozen>
{
  static CUserMsg_ParticleManager_UpdateParticleSetFrozen ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleSetFrozen>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleSetFrozenImpl(handle, isManuallyAllocated);


  public bool SetFrozen { get; set; }


  public float TransitionDuration { get; set; }

}
