
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_WatchInfoUsersImpl : TypedProtobuf<CMsgGCCStrike15_v2_WatchInfoUsers>, CMsgGCCStrike15_v2_WatchInfoUsers
{
  public CMsgGCCStrike15_v2_WatchInfoUsersImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint RequestId
  { get => Accessor.GetUInt32("request_id"); set => Accessor.SetUInt32("request_id", value); }


  public IProtobufRepeatedFieldValueType<uint> AccountIds
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "account_ids"); }


  public IProtobufRepeatedFieldSubMessageType<WatchableMatchInfo> WatchableMatchInfos
  { get => new ProtobufRepeatedFieldSubMessageType<WatchableMatchInfo>(Accessor, "watchable_match_infos"); }


  public uint ExtendedTimeout
  { get => Accessor.GetUInt32("extended_timeout"); set => Accessor.SetUInt32("extended_timeout", value); }

}
