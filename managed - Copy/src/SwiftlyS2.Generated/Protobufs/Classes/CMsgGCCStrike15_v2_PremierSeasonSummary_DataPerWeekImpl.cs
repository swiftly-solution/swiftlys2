
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeekImpl : TypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek>, CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek
{
  public CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeekImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong WeekId
  { get => Accessor.GetUInt64("week_id"); set => Accessor.SetUInt64("week_id", value); }


  public uint RankId
  { get => Accessor.GetUInt32("rank_id"); set => Accessor.SetUInt32("rank_id", value); }


  public uint MatchesPlayed
  { get => Accessor.GetUInt32("matches_played"); set => Accessor.SetUInt32("matches_played", value); }

}
