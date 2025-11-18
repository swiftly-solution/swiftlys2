
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetControlPointSnapshot : ITypedProtobuf<CUserMsg_ParticleManager_SetControlPointSnapshot>
{
  static CUserMsg_ParticleManager_SetControlPointSnapshot ITypedProtobuf<CUserMsg_ParticleManager_SetControlPointSnapshot>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetControlPointSnapshotImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public string SnapshotName { get; set; }

}
