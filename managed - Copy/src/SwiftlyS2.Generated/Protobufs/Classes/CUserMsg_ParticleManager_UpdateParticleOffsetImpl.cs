
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleOffsetImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleOffset>, CUserMsg_ParticleManager_UpdateParticleOffset
{
  public CUserMsg_ParticleManager_UpdateParticleOffsetImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public Vector OriginOffset
  { get => Accessor.GetVector("origin_offset"); set => Accessor.SetVector("origin_offset", value); }


  public QAngle AngleOffset
  { get => Accessor.GetQAngle("angle_offset"); set => Accessor.SetQAngle("angle_offset", value); }

}
