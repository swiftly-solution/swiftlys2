
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCReportAbuseImpl : TypedProtobuf<CMsgGCReportAbuse>, CMsgGCReportAbuse
{
  public CMsgGCReportAbuseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong TargetSteamId
  { get => Accessor.GetUInt64("target_steam_id"); set => Accessor.SetUInt64("target_steam_id", value); }


  public string Description
  { get => Accessor.GetString("description"); set => Accessor.SetString("description", value); }


  public ulong Gid
  { get => Accessor.GetUInt64("gid"); set => Accessor.SetUInt64("gid", value); }


  public uint AbuseType
  { get => Accessor.GetUInt32("abuse_type"); set => Accessor.SetUInt32("abuse_type", value); }


  public uint ContentType
  { get => Accessor.GetUInt32("content_type"); set => Accessor.SetUInt32("content_type", value); }


  public uint TargetGameServerIp
  { get => Accessor.GetUInt32("target_game_server_ip"); set => Accessor.SetUInt32("target_game_server_ip", value); }


  public uint TargetGameServerPort
  { get => Accessor.GetUInt32("target_game_server_port"); set => Accessor.SetUInt32("target_game_server_port", value); }

}
