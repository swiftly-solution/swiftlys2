
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_DiagnosticImpl : NetMessage<CCLCMsg_Diagnostic>, CCLCMsg_Diagnostic
{
  public CCLCMsg_DiagnosticImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgSource2SystemSpecs SystemSpecs
  { get => new CMsgSource2SystemSpecsImpl(NativeNetMessages.GetNestedMessage(Address, "system_specs"), false); }


  public CMsgSource2VProfLiteReport VprofReport
  { get => new CMsgSource2VProfLiteReportImpl(NativeNetMessages.GetNestedMessage(Address, "vprof_report"), false); }


  public CMsgSource2NetworkFlowQuality DownstreamFlow
  { get => new CMsgSource2NetworkFlowQualityImpl(NativeNetMessages.GetNestedMessage(Address, "downstream_flow"), false); }


  public CMsgSource2NetworkFlowQuality UpstreamFlow
  { get => new CMsgSource2NetworkFlowQualityImpl(NativeNetMessages.GetNestedMessage(Address, "upstream_flow"), false); }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample> PerfSamples
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample>(Accessor, "perf_samples"); }

}
