
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GiftsLeaderboardRequest : ITypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardRequest>
{
  static CMsgGCCStrike15_v2_GiftsLeaderboardRequest ITypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GiftsLeaderboardRequestImpl(handle, isManuallyAllocated);


}
