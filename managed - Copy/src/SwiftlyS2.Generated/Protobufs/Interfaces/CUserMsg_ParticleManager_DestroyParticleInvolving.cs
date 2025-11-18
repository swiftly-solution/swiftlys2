
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_DestroyParticleInvolving : ITypedProtobuf<CUserMsg_ParticleManager_DestroyParticleInvolving>
{
  static CUserMsg_ParticleManager_DestroyParticleInvolving ITypedProtobuf<CUserMsg_ParticleManager_DestroyParticleInvolving>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_DestroyParticleInvolvingImpl(handle, isManuallyAllocated);


  public bool DestroyImmediately { get; set; }


  public uint EntityHandle { get; set; }

}
