
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2PerfIntervalSample : ITypedProtobuf<CMsgSource2PerfIntervalSample>
{
  static CMsgSource2PerfIntervalSample ITypedProtobuf<CMsgSource2PerfIntervalSample>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2PerfIntervalSampleImpl(handle, isManuallyAllocated);


  public float FrameTimeMaxMs { get; set; }


  public float FrameTimeAvgMs { get; set; }


  public float FrameTimeMinMs { get; set; }


  public int FrameCount { get; set; }


  public float FrameTimeTotalMs { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample_Tag> Tags { get; }

}
