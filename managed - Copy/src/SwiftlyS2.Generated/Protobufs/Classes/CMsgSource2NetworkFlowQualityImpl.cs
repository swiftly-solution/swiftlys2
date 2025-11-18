
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource2NetworkFlowQualityImpl : TypedProtobuf<CMsgSource2NetworkFlowQuality>, CMsgSource2NetworkFlowQuality
{
  public CMsgSource2NetworkFlowQualityImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Duration
  { get => Accessor.GetUInt32("duration"); set => Accessor.SetUInt32("duration", value); }


  public ulong BytesTotal
  { get => Accessor.GetUInt64("bytes_total"); set => Accessor.SetUInt64("bytes_total", value); }


  public ulong BytesTotalReliable
  { get => Accessor.GetUInt64("bytes_total_reliable"); set => Accessor.SetUInt64("bytes_total_reliable", value); }


  public ulong BytesTotalVoice
  { get => Accessor.GetUInt64("bytes_total_voice"); set => Accessor.SetUInt64("bytes_total_voice", value); }


  public uint BytesSecP95
  { get => Accessor.GetUInt32("bytes_sec_p95"); set => Accessor.SetUInt32("bytes_sec_p95", value); }


  public uint BytesSecP99
  { get => Accessor.GetUInt32("bytes_sec_p99"); set => Accessor.SetUInt32("bytes_sec_p99", value); }


  public uint EnginemsgsTotal
  { get => Accessor.GetUInt32("enginemsgs_total"); set => Accessor.SetUInt32("enginemsgs_total", value); }


  public uint EnginemsgsSecP95
  { get => Accessor.GetUInt32("enginemsgs_sec_p95"); set => Accessor.SetUInt32("enginemsgs_sec_p95", value); }


  public uint EnginemsgsSecP99
  { get => Accessor.GetUInt32("enginemsgs_sec_p99"); set => Accessor.SetUInt32("enginemsgs_sec_p99", value); }


  public uint NetframesTotal
  { get => Accessor.GetUInt32("netframes_total"); set => Accessor.SetUInt32("netframes_total", value); }


  public uint NetframesDropped
  { get => Accessor.GetUInt32("netframes_dropped"); set => Accessor.SetUInt32("netframes_dropped", value); }


  public uint NetframesOutoforder
  { get => Accessor.GetUInt32("netframes_outoforder"); set => Accessor.SetUInt32("netframes_outoforder", value); }


  public uint NetframesSizeExceedsMtu
  { get => Accessor.GetUInt32("netframes_size_exceeds_mtu"); set => Accessor.SetUInt32("netframes_size_exceeds_mtu", value); }


  public uint NetframesSizeP95
  { get => Accessor.GetUInt32("netframes_size_p95"); set => Accessor.SetUInt32("netframes_size_p95", value); }


  public uint NetframesSizeP99
  { get => Accessor.GetUInt32("netframes_size_p99"); set => Accessor.SetUInt32("netframes_size_p99", value); }


  public uint TicksTotal
  { get => Accessor.GetUInt32("ticks_total"); set => Accessor.SetUInt32("ticks_total", value); }


  public uint TicksGood
  { get => Accessor.GetUInt32("ticks_good"); set => Accessor.SetUInt32("ticks_good", value); }


  public uint TicksGoodAlmostLate
  { get => Accessor.GetUInt32("ticks_good_almost_late"); set => Accessor.SetUInt32("ticks_good_almost_late", value); }


  public uint TicksFixedDropped
  { get => Accessor.GetUInt32("ticks_fixed_dropped"); set => Accessor.SetUInt32("ticks_fixed_dropped", value); }


  public uint TicksFixedLate
  { get => Accessor.GetUInt32("ticks_fixed_late"); set => Accessor.SetUInt32("ticks_fixed_late", value); }


  public uint TicksBadDropped
  { get => Accessor.GetUInt32("ticks_bad_dropped"); set => Accessor.SetUInt32("ticks_bad_dropped", value); }


  public uint TicksBadLate
  { get => Accessor.GetUInt32("ticks_bad_late"); set => Accessor.SetUInt32("ticks_bad_late", value); }


  public uint TicksBadOther
  { get => Accessor.GetUInt32("ticks_bad_other"); set => Accessor.SetUInt32("ticks_bad_other", value); }


  public uint TickMissrateSamplesTotal
  { get => Accessor.GetUInt32("tick_missrate_samples_total"); set => Accessor.SetUInt32("tick_missrate_samples_total", value); }


  public uint TickMissrateSamplesPerfect
  { get => Accessor.GetUInt32("tick_missrate_samples_perfect"); set => Accessor.SetUInt32("tick_missrate_samples_perfect", value); }


  public uint TickMissrateSamplesPerfectnet
  { get => Accessor.GetUInt32("tick_missrate_samples_perfectnet"); set => Accessor.SetUInt32("tick_missrate_samples_perfectnet", value); }


  public uint TickMissratenetP75X10
  { get => Accessor.GetUInt32("tick_missratenet_p75_x10"); set => Accessor.SetUInt32("tick_missratenet_p75_x10", value); }


  public uint TickMissratenetP95X10
  { get => Accessor.GetUInt32("tick_missratenet_p95_x10"); set => Accessor.SetUInt32("tick_missratenet_p95_x10", value); }


  public uint TickMissratenetP99X10
  { get => Accessor.GetUInt32("tick_missratenet_p99_x10"); set => Accessor.SetUInt32("tick_missratenet_p99_x10", value); }


  public int RecvmarginP1
  { get => Accessor.GetInt32("recvmargin_p1"); set => Accessor.SetInt32("recvmargin_p1", value); }


  public int RecvmarginP5
  { get => Accessor.GetInt32("recvmargin_p5"); set => Accessor.SetInt32("recvmargin_p5", value); }


  public int RecvmarginP25
  { get => Accessor.GetInt32("recvmargin_p25"); set => Accessor.SetInt32("recvmargin_p25", value); }


  public int RecvmarginP50
  { get => Accessor.GetInt32("recvmargin_p50"); set => Accessor.SetInt32("recvmargin_p50", value); }


  public int RecvmarginP75
  { get => Accessor.GetInt32("recvmargin_p75"); set => Accessor.SetInt32("recvmargin_p75", value); }


  public int RecvmarginP95
  { get => Accessor.GetInt32("recvmargin_p95"); set => Accessor.SetInt32("recvmargin_p95", value); }


  public uint NetframeJitterP50
  { get => Accessor.GetUInt32("netframe_jitter_p50"); set => Accessor.SetUInt32("netframe_jitter_p50", value); }


  public uint NetframeJitterP99
  { get => Accessor.GetUInt32("netframe_jitter_p99"); set => Accessor.SetUInt32("netframe_jitter_p99", value); }


  public uint IntervalPeakjitterP50
  { get => Accessor.GetUInt32("interval_peakjitter_p50"); set => Accessor.SetUInt32("interval_peakjitter_p50", value); }


  public uint IntervalPeakjitterP95
  { get => Accessor.GetUInt32("interval_peakjitter_p95"); set => Accessor.SetUInt32("interval_peakjitter_p95", value); }


  public uint PacketMisdeliveryRateP50X4
  { get => Accessor.GetUInt32("packet_misdelivery_rate_p50_x4"); set => Accessor.SetUInt32("packet_misdelivery_rate_p50_x4", value); }


  public uint PacketMisdeliveryRateP95X4
  { get => Accessor.GetUInt32("packet_misdelivery_rate_p95_x4"); set => Accessor.SetUInt32("packet_misdelivery_rate_p95_x4", value); }


  public uint NetPingP5
  { get => Accessor.GetUInt32("net_ping_p5"); set => Accessor.SetUInt32("net_ping_p5", value); }


  public uint NetPingP50
  { get => Accessor.GetUInt32("net_ping_p50"); set => Accessor.SetUInt32("net_ping_p50", value); }


  public uint NetPingP95
  { get => Accessor.GetUInt32("net_ping_p95"); set => Accessor.SetUInt32("net_ping_p95", value); }

}
