
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticle_OBSOLETEImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticle_OBSOLETE>, CUserMsg_ParticleManager_UpdateParticle_OBSOLETE
{
  public CUserMsg_ParticleManager_UpdateParticle_OBSOLETEImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public Vector Position
  { get => Accessor.GetVector("position"); set => Accessor.SetVector("position", value); }

}
