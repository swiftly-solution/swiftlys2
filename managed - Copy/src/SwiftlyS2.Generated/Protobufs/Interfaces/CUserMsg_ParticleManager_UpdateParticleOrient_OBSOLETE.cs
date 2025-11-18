
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE>
{
  static CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETEImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public Vector Forward { get; set; }


  public Vector DeprecatedRight { get; set; }


  public Vector Up { get; set; }


  public Vector Left { get; set; }

}
