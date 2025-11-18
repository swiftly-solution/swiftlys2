
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientRequestJoinServerDataImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientRequestJoinServerData>, CMsgGCCStrike15_v2_ClientRequestJoinServerData
{
  public CMsgGCCStrike15_v2_ClientRequestJoinServerDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Version
  { get => Accessor.GetUInt32("version"); set => Accessor.SetUInt32("version", value); }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public ulong Serverid
  { get => Accessor.GetUInt64("serverid"); set => Accessor.SetUInt64("serverid", value); }


  public uint ServerIp
  { get => Accessor.GetUInt32("server_ip"); set => Accessor.SetUInt32("server_ip", value); }


  public uint ServerPort
  { get => Accessor.GetUInt32("server_port"); set => Accessor.SetUInt32("server_port", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve Res
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl(NativeNetMessages.GetNestedMessage(Address, "res"), false); }


  public string Errormsg
  { get => Accessor.GetString("errormsg"); set => Accessor.SetString("errormsg", value); }

}
