
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_DesiredTimescaleImpl : NetMessage<CCSUsrMsg_DesiredTimescale>, CCSUsrMsg_DesiredTimescale
{
  public CCSUsrMsg_DesiredTimescaleImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float DesiredTimescale
  { get => Accessor.GetFloat("desired_timescale"); set => Accessor.SetFloat("desired_timescale", value); }


  public float DurationRealtimeSec
  { get => Accessor.GetFloat("duration_realtime_sec"); set => Accessor.SetFloat("duration_realtime_sec", value); }


  public int InterpolatorType
  { get => Accessor.GetInt32("interpolator_type"); set => Accessor.SetInt32("interpolator_type", value); }


  public float StartBlendTime
  { get => Accessor.GetFloat("start_blend_time"); set => Accessor.SetFloat("start_blend_time", value); }

}
