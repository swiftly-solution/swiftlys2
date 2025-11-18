
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_FreezeParticleInvolving : ITypedProtobuf<CUserMsg_ParticleManager_FreezeParticleInvolving>
{
  static CUserMsg_ParticleManager_FreezeParticleInvolving ITypedProtobuf<CUserMsg_ParticleManager_FreezeParticleInvolving>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_FreezeParticleInvolvingImpl(handle, isManuallyAllocated);


  public bool SetFrozen { get; set; }


  public float TransitionDuration { get; set; }


  public uint EntityHandle { get; set; }

}
