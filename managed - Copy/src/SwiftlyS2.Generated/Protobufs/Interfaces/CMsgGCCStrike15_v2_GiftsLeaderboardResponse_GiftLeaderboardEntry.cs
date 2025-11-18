
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry : ITypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry>
{
  static CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry ITypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntryImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public uint Gifts { get; set; }

}
