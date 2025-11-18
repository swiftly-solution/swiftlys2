
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_AddFanImpl : TypedProtobuf<CUserMsg_ParticleManager_AddFan>, CUserMsg_ParticleManager_AddFan
{
  public CUserMsg_ParticleManager_AddFanImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Active
  { get => Accessor.GetBool("active"); set => Accessor.SetBool("active", value); }


  public Vector BoundsMins
  { get => Accessor.GetVector("bounds_mins"); set => Accessor.SetVector("bounds_mins", value); }


  public Vector BoundsMaxs
  { get => Accessor.GetVector("bounds_maxs"); set => Accessor.SetVector("bounds_maxs", value); }


  public Vector FanOrigin
  { get => Accessor.GetVector("fan_origin"); set => Accessor.SetVector("fan_origin", value); }


  public Vector FanOriginOffset
  { get => Accessor.GetVector("fan_origin_offset"); set => Accessor.SetVector("fan_origin_offset", value); }


  public Vector FanDirection
  { get => Accessor.GetVector("fan_direction"); set => Accessor.SetVector("fan_direction", value); }


  public float Force
  { get => Accessor.GetFloat("force"); set => Accessor.SetFloat("force", value); }


  public string FanForceCurve
  { get => Accessor.GetString("fan_force_curve"); set => Accessor.SetString("fan_force_curve", value); }


  public bool Falloff
  { get => Accessor.GetBool("falloff"); set => Accessor.SetBool("falloff", value); }


  public bool PullTowardsPoint
  { get => Accessor.GetBool("pull_towards_point"); set => Accessor.SetBool("pull_towards_point", value); }


  public float CurveMinDist
  { get => Accessor.GetFloat("curve_min_dist"); set => Accessor.SetFloat("curve_min_dist", value); }


  public float CurveMaxDist
  { get => Accessor.GetFloat("curve_max_dist"); set => Accessor.SetFloat("curve_max_dist", value); }


  public uint FanType
  { get => Accessor.GetUInt32("fan_type"); set => Accessor.SetUInt32("fan_type", value); }


  public float ConeStartRadius
  { get => Accessor.GetFloat("cone_start_radius"); set => Accessor.SetFloat("cone_start_radius", value); }


  public float ConeEndRadius
  { get => Accessor.GetFloat("cone_end_radius"); set => Accessor.SetFloat("cone_end_radius", value); }


  public float ConeLength
  { get => Accessor.GetFloat("cone_length"); set => Accessor.SetFloat("cone_length", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }


  public string AttachmentName
  { get => Accessor.GetString("attachment_name"); set => Accessor.SetString("attachment_name", value); }

}
