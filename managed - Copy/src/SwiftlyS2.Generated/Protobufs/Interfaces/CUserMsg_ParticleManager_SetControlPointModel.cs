
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetControlPointModel : ITypedProtobuf<CUserMsg_ParticleManager_SetControlPointModel>
{
  static CUserMsg_ParticleManager_SetControlPointModel ITypedProtobuf<CUserMsg_ParticleManager_SetControlPointModel>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetControlPointModelImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public string ModelName { get; set; }

}
