
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntryImpl : TypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry>, CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry
{
  public CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public uint Gifts
  { get => Accessor.GetUInt32("gifts"); set => Accessor.SetUInt32("gifts", value); }

}
