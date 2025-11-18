
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleTransform : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleTransform>
{
  static CUserMsg_ParticleManager_UpdateParticleTransform ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleTransform>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleTransformImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public Vector Position { get; set; }


  public CMsgQuaternion Orientation { get; }


  public float InterpolationInterval { get; set; }

}
