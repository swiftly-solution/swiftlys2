
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSource2Metrics_MatchPerfSummary_Notification : ITypedProtobuf<CSource2Metrics_MatchPerfSummary_Notification>
{
  static CSource2Metrics_MatchPerfSummary_Notification ITypedProtobuf<CSource2Metrics_MatchPerfSummary_Notification>.Wrap(nint handle, bool isManuallyAllocated) => new CSource2Metrics_MatchPerfSummary_NotificationImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public string GameMode { get; set; }


  public uint ServerBuildId { get; set; }


  public uint ServerPopid { get; set; }


  public CMsgSource2VProfLiteReport ServerProfile { get; }


  public IProtobufRepeatedFieldSubMessageType<CSource2Metrics_MatchPerfSummary_Notification_Client> Clients { get; }


  public string Map { get; set; }

}
