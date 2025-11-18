
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientRequestJoinFriendDataImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientRequestJoinFriendData>, CMsgGCCStrike15_v2_ClientRequestJoinFriendData
{
  public CMsgGCCStrike15_v2_ClientRequestJoinFriendDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Version
  { get => Accessor.GetUInt32("version"); set => Accessor.SetUInt32("version", value); }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint JoinToken
  { get => Accessor.GetUInt32("join_token"); set => Accessor.SetUInt32("join_token", value); }


  public uint JoinIpp
  { get => Accessor.GetUInt32("join_ipp"); set => Accessor.SetUInt32("join_ipp", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve Res
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl(NativeNetMessages.GetNestedMessage(Address, "res"), false); }


  public string Errormsg
  { get => Accessor.GetString("errormsg"); set => Accessor.SetString("errormsg", value); }

}
