
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSource2Metrics_MatchPerfSummary_NotificationImpl : TypedProtobuf<CSource2Metrics_MatchPerfSummary_Notification>, CSource2Metrics_MatchPerfSummary_Notification
{
  public CSource2Metrics_MatchPerfSummary_NotificationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public string GameMode
  { get => Accessor.GetString("game_mode"); set => Accessor.SetString("game_mode", value); }


  public uint ServerBuildId
  { get => Accessor.GetUInt32("server_build_id"); set => Accessor.SetUInt32("server_build_id", value); }


  public uint ServerPopid
  { get => Accessor.GetUInt32("server_popid"); set => Accessor.SetUInt32("server_popid", value); }


  public CMsgSource2VProfLiteReport ServerProfile
  { get => new CMsgSource2VProfLiteReportImpl(NativeNetMessages.GetNestedMessage(Address, "server_profile"), false); }


  public IProtobufRepeatedFieldSubMessageType<CSource2Metrics_MatchPerfSummary_Notification_Client> Clients
  { get => new ProtobufRepeatedFieldSubMessageType<CSource2Metrics_MatchPerfSummary_Notification_Client>(Accessor, "clients"); }


  public string Map
  { get => Accessor.GetString("map"); set => Accessor.SetString("map", value); }

}
