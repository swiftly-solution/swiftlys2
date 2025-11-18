
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchEndRunRewardDropsImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchEndRunRewardDrops>, CMsgGCCStrike15_v2_MatchEndRunRewardDrops
{
  public CMsgGCCStrike15_v2_MatchEndRunRewardDropsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CMsgGCCStrike15_v2_MatchmakingServerReservationResponse Serverinfo
  { get => new CMsgGCCStrike15_v2_MatchmakingServerReservationResponseImpl(NativeNetMessages.GetNestedMessage(Address, "serverinfo"), false); }


  public CMsgGC_ServerQuestUpdateData MatchEndQuestData
  { get => new CMsgGC_ServerQuestUpdateDataImpl(NativeNetMessages.GetNestedMessage(Address, "match_end_quest_data"), false); }

}
