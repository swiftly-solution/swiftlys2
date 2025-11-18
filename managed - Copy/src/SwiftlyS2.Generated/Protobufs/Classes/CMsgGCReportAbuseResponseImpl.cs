
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCReportAbuseResponseImpl : TypedProtobuf<CMsgGCReportAbuseResponse>, CMsgGCReportAbuseResponse
{
  public CMsgGCReportAbuseResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong TargetSteamId
  { get => Accessor.GetUInt64("target_steam_id"); set => Accessor.SetUInt64("target_steam_id", value); }


  public uint Result
  { get => Accessor.GetUInt32("result"); set => Accessor.SetUInt32("result", value); }


  public string ErrorMessage
  { get => Accessor.GetString("error_message"); set => Accessor.SetString("error_message", value); }

}
