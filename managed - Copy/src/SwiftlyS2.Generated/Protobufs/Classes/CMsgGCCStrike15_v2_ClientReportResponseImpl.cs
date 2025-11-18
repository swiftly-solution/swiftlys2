
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientReportResponseImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientReportResponse>, CMsgGCCStrike15_v2_ClientReportResponse
{
  public CMsgGCCStrike15_v2_ClientReportResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ConfirmationId
  { get => Accessor.GetUInt64("confirmation_id"); set => Accessor.SetUInt64("confirmation_id", value); }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint ServerIp
  { get => Accessor.GetUInt32("server_ip"); set => Accessor.SetUInt32("server_ip", value); }


  public uint ResponseType
  { get => Accessor.GetUInt32("response_type"); set => Accessor.SetUInt32("response_type", value); }


  public uint ResponseResult
  { get => Accessor.GetUInt32("response_result"); set => Accessor.SetUInt32("response_result", value); }


  public uint Tokens
  { get => Accessor.GetUInt32("tokens"); set => Accessor.SetUInt32("tokens", value); }

}
