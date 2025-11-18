
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE>
{
  static CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETEImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public Vector Forward { get; set; }

}
