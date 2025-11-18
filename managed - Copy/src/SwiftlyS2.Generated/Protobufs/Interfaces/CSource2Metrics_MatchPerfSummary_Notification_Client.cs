
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSource2Metrics_MatchPerfSummary_Notification_Client : ITypedProtobuf<CSource2Metrics_MatchPerfSummary_Notification_Client>
{
  static CSource2Metrics_MatchPerfSummary_Notification_Client ITypedProtobuf<CSource2Metrics_MatchPerfSummary_Notification_Client>.Wrap(nint handle, bool isManuallyAllocated) => new CSource2Metrics_MatchPerfSummary_Notification_ClientImpl(handle, isManuallyAllocated);


  public CMsgSource2SystemSpecs SystemSpecs { get; }


  public CMsgSource2VProfLiteReport Profile { get; }


  public uint BuildId { get; set; }


  public CMsgSource2NetworkFlowQuality DownstreamFlow { get; }


  public CMsgSource2NetworkFlowQuality UpstreamFlow { get; }


  public ulong Steamid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource2PerfIntervalSample> PerfSamples { get; }

}
