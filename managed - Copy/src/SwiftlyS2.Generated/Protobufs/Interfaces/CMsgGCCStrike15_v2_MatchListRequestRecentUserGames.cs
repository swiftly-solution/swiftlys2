
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchListRequestRecentUserGames : ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestRecentUserGames>
{
  static CMsgGCCStrike15_v2_MatchListRequestRecentUserGames ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestRecentUserGames>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchListRequestRecentUserGamesImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }

}
