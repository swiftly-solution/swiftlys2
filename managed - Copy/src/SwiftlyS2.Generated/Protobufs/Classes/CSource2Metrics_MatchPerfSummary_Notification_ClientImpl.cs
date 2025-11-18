
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSource2Metrics_MatchPerfSummary_Notification_ClientImpl : TypedProtobuf<CSource2Metrics_MatchPerfSummary_Notification_Client>, CSource2Metrics_MatchPerfSummary_Notification_Client
{
  public CSource2Metrics_MatchPerfSummary_Notification_ClientImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CMsgSource2SystemSpecs SystemSpecs
  { get => new CMsgSource2SystemSpecsImpl(NativeNetMessages.GetNestedMessage(Address, "system_specs"), false); }


  public CMsgSource2VProfLiteReport Profile
  { get => new CMsgSource2VProfLiteReportImpl(NativeNetMessages.GetNestedMessage(Address, "profile"), false); }


  public uint BuildId
  { get => Accessor.GetUInt32("build_id"); set => Accessor.SetUInt32("build_id", value); }


  public CMsgSource2NetworkFlowQuality DownstreamFlow
  { get => new CMsgSource2NetworkFlowQualityImpl(NativeNetMessages.GetNestedMessage(Address, "downstream_flow"), false); }


  public CMsgSource2NetworkFlowQuality UpstreamFlow
  { get => new CMsgSource2NetworkFlowQualityImpl(NativeNetMessages.GetNestedMessage(Address, "upstream_flow"), false); }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample> PerfSamples
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample>(Accessor, "perf_samples"); }

}
