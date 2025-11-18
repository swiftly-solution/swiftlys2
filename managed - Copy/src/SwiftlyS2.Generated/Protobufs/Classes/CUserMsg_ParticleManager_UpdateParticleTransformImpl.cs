
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleTransformImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleTransform>, CUserMsg_ParticleManager_UpdateParticleTransform
{
  public CUserMsg_ParticleManager_UpdateParticleTransformImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public Vector Position
  { get => Accessor.GetVector("position"); set => Accessor.SetVector("position", value); }


  public CMsgQuaternion Orientation
  { get => new CMsgQuaternionImpl(NativeNetMessages.GetNestedMessage(Address, "orientation"), false); }


  public float InterpolationInterval
  { get => Accessor.GetFloat("interpolation_interval"); set => Accessor.SetFloat("interpolation_interval", value); }

}
