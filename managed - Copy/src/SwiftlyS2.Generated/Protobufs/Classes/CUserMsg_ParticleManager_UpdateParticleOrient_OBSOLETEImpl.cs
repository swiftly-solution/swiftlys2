
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETEImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE>, CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE
{
  public CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETEImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public Vector Forward
  { get => Accessor.GetVector("forward"); set => Accessor.SetVector("forward", value); }


  public Vector DeprecatedRight
  { get => Accessor.GetVector("deprecated_right"); set => Accessor.SetVector("deprecated_right", value); }


  public Vector Up
  { get => Accessor.GetVector("up"); set => Accessor.SetVector("up", value); }


  public Vector Left
  { get => Accessor.GetVector("left"); set => Accessor.SetVector("left", value); }

}
