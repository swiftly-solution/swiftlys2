
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_SetPlayerLeaderboardSafeNameImpl : TypedProtobuf<CMsgGCCStrike15_v2_SetPlayerLeaderboardSafeName>, CMsgGCCStrike15_v2_SetPlayerLeaderboardSafeName
{
  public CMsgGCCStrike15_v2_SetPlayerLeaderboardSafeNameImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string LeaderboardSafeName
  { get => Accessor.GetString("leaderboard_safe_name"); set => Accessor.SetString("leaderboard_safe_name", value); }

}
