
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource2PerfIntervalSampleImpl : TypedProtobuf<CMsgSource2PerfIntervalSample>, CMsgSource2PerfIntervalSample
{
  public CMsgSource2PerfIntervalSampleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float FrameTimeMaxMs
  { get => Accessor.GetFloat("frame_time_max_ms"); set => Accessor.SetFloat("frame_time_max_ms", value); }


  public float FrameTimeAvgMs
  { get => Accessor.GetFloat("frame_time_avg_ms"); set => Accessor.SetFloat("frame_time_avg_ms", value); }


  public float FrameTimeMinMs
  { get => Accessor.GetFloat("frame_time_min_ms"); set => Accessor.SetFloat("frame_time_min_ms", value); }


  public int FrameCount
  { get => Accessor.GetInt32("frame_count"); set => Accessor.SetInt32("frame_count", value); }


  public float FrameTimeTotalMs
  { get => Accessor.GetFloat("frame_time_total_ms"); set => Accessor.SetFloat("frame_time_total_ms", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample_Tag> Tags
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample_Tag>(Accessor, "tags"); }

}
