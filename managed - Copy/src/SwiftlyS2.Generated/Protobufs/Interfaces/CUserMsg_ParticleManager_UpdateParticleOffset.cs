
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleOffset : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleOffset>
{
  static CUserMsg_ParticleManager_UpdateParticleOffset ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleOffset>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleOffsetImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public Vector OriginOffset { get; set; }


  public QAngle AngleOffset { get; set; }

}
