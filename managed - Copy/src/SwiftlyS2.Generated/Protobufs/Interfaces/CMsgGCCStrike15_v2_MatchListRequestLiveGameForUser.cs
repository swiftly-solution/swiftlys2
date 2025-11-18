
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchListRequestLiveGameForUser : ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestLiveGameForUser>
{
  static CMsgGCCStrike15_v2_MatchListRequestLiveGameForUser ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestLiveGameForUser>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchListRequestLiveGameForUserImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }

}
