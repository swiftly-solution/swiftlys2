
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_ParticleCanFreeze : ITypedProtobuf<CUserMsg_ParticleManager_ParticleCanFreeze>
{
  static CUserMsg_ParticleManager_ParticleCanFreeze ITypedProtobuf<CUserMsg_ParticleManager_ParticleCanFreeze>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_ParticleCanFreezeImpl(handle, isManuallyAllocated);


  public bool CanFreeze { get; set; }

}
