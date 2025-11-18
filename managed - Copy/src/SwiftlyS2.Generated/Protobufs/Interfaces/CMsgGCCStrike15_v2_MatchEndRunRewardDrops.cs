
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchEndRunRewardDrops : ITypedProtobuf<CMsgGCCStrike15_v2_MatchEndRunRewardDrops>
{
  static CMsgGCCStrike15_v2_MatchEndRunRewardDrops ITypedProtobuf<CMsgGCCStrike15_v2_MatchEndRunRewardDrops>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchEndRunRewardDropsImpl(handle, isManuallyAllocated);


  public CMsgGCCStrike15_v2_MatchmakingServerReservationResponse Serverinfo { get; }


  public CMsgGC_ServerQuestUpdateData MatchEndQuestData { get; }

}
