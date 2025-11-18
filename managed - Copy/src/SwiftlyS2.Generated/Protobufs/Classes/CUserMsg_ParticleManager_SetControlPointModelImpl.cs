
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetControlPointModelImpl : TypedProtobuf<CUserMsg_ParticleManager_SetControlPointModel>, CUserMsg_ParticleManager_SetControlPointModel
{
  public CUserMsg_ParticleManager_SetControlPointModelImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public string ModelName
  { get => Accessor.GetString("model_name"); set => Accessor.SetString("model_name", value); }

}
