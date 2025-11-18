
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_ParticleSkipToTime : ITypedProtobuf<CUserMsg_ParticleManager_ParticleSkipToTime>
{
  static CUserMsg_ParticleManager_ParticleSkipToTime ITypedProtobuf<CUserMsg_ParticleManager_ParticleSkipToTime>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_ParticleSkipToTimeImpl(handle, isManuallyAllocated);


  public float SkipToTime { get; set; }

}
