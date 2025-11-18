
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateFanImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateFan>, CUserMsg_ParticleManager_UpdateFan
{
  public CUserMsg_ParticleManager_UpdateFanImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Active
  { get => Accessor.GetBool("active"); set => Accessor.SetBool("active", value); }


  public Vector FanOrigin
  { get => Accessor.GetVector("fan_origin"); set => Accessor.SetVector("fan_origin", value); }


  public Vector FanOriginOffset
  { get => Accessor.GetVector("fan_origin_offset"); set => Accessor.SetVector("fan_origin_offset", value); }


  public Vector FanDirection
  { get => Accessor.GetVector("fan_direction"); set => Accessor.SetVector("fan_direction", value); }


  public float FanRampRatio
  { get => Accessor.GetFloat("fan_ramp_ratio"); set => Accessor.SetFloat("fan_ramp_ratio", value); }


  public Vector BoundsMins
  { get => Accessor.GetVector("bounds_mins"); set => Accessor.SetVector("bounds_mins", value); }


  public Vector BoundsMaxs
  { get => Accessor.GetVector("bounds_maxs"); set => Accessor.SetVector("bounds_maxs", value); }

}
