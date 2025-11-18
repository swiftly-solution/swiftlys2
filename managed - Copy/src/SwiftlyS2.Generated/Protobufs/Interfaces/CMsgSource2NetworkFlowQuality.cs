
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource2NetworkFlowQuality : ITypedProtobuf<CMsgSource2NetworkFlowQuality>
{
  static CMsgSource2NetworkFlowQuality ITypedProtobuf<CMsgSource2NetworkFlowQuality>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource2NetworkFlowQualityImpl(handle, isManuallyAllocated);


  public uint Duration { get; set; }


  public ulong BytesTotal { get; set; }


  public ulong BytesTotalReliable { get; set; }


  public ulong BytesTotalVoice { get; set; }


  public uint BytesSecP95 { get; set; }


  public uint BytesSecP99 { get; set; }


  public uint EnginemsgsTotal { get; set; }


  public uint EnginemsgsSecP95 { get; set; }


  public uint EnginemsgsSecP99 { get; set; }


  public uint NetframesTotal { get; set; }


  public uint NetframesDropped { get; set; }


  public uint NetframesOutoforder { get; set; }


  public uint NetframesSizeExceedsMtu { get; set; }


  public uint NetframesSizeP95 { get; set; }


  public uint NetframesSizeP99 { get; set; }


  public uint TicksTotal { get; set; }


  public uint TicksGood { get; set; }


  public uint TicksGoodAlmostLate { get; set; }


  public uint TicksFixedDropped { get; set; }


  public uint TicksFixedLate { get; set; }


  public uint TicksBadDropped { get; set; }


  public uint TicksBadLate { get; set; }


  public uint TicksBadOther { get; set; }


  public uint TickMissrateSamplesTotal { get; set; }


  public uint TickMissrateSamplesPerfect { get; set; }


  public uint TickMissrateSamplesPerfectnet { get; set; }


  public uint TickMissratenetP75X10 { get; set; }


  public uint TickMissratenetP95X10 { get; set; }


  public uint TickMissratenetP99X10 { get; set; }


  public int RecvmarginP1 { get; set; }


  public int RecvmarginP5 { get; set; }


  public int RecvmarginP25 { get; set; }


  public int RecvmarginP50 { get; set; }


  public int RecvmarginP75 { get; set; }


  public int RecvmarginP95 { get; set; }


  public uint NetframeJitterP50 { get; set; }


  public uint NetframeJitterP99 { get; set; }


  public uint IntervalPeakjitterP50 { get; set; }


  public uint IntervalPeakjitterP95 { get; set; }


  public uint PacketMisdeliveryRateP50X4 { get; set; }


  public uint PacketMisdeliveryRateP95X4 { get; set; }


  public uint NetPingP5 { get; set; }


  public uint NetPingP50 { get; set; }


  public uint NetPingP95 { get; set; }

}
