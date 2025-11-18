
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleFoWPropertiesImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleFoWProperties>, CUserMsg_ParticleManager_SetParticleFoWProperties
{
  public CUserMsg_ParticleManager_SetParticleFoWPropertiesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int FowControlPoint
  { get => Accessor.GetInt32("fow_control_point"); set => Accessor.SetInt32("fow_control_point", value); }


  public int FowControlPoint2
  { get => Accessor.GetInt32("fow_control_point2"); set => Accessor.SetInt32("fow_control_point2", value); }


  public float FowRadius
  { get => Accessor.GetFloat("fow_radius"); set => Accessor.SetFloat("fow_radius", value); }

}
