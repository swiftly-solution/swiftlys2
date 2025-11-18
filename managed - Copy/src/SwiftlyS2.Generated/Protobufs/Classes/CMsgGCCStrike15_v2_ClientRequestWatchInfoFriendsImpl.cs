
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientRequestWatchInfoFriendsImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientRequestWatchInfoFriends>, CMsgGCCStrike15_v2_ClientRequestWatchInfoFriends
{
  public CMsgGCCStrike15_v2_ClientRequestWatchInfoFriendsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint RequestId
  { get => Accessor.GetUInt32("request_id"); set => Accessor.SetUInt32("request_id", value); }


  public IProtobufRepeatedFieldValueType<uint> AccountIds
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "account_ids"); }


  public ulong Serverid
  { get => Accessor.GetUInt64("serverid"); set => Accessor.SetUInt64("serverid", value); }


  public ulong Matchid
  { get => Accessor.GetUInt64("matchid"); set => Accessor.SetUInt64("matchid", value); }


  public uint ClientLauncher
  { get => Accessor.GetUInt32("client_launcher"); set => Accessor.SetUInt32("client_launcher", value); }


  public IProtobufRepeatedFieldSubMessageType<DataCenterPing> DataCenterPings
  { get => new ProtobufRepeatedFieldSubMessageType<DataCenterPing>(Accessor, "data_center_pings"); }

}
