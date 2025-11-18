
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETEImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE>, CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE
{
  public CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETEImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public Vector Forward
  { get => Accessor.GetVector("forward"); set => Accessor.SetVector("forward", value); }

}
