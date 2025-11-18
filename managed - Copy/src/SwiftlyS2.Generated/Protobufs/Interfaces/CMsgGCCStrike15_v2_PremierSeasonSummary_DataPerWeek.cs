
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek : ITypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek>
{
  static CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek ITypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeekImpl(handle, isManuallyAllocated);


  public ulong WeekId { get; set; }


  public uint RankId { get; set; }


  public uint MatchesPlayed { get; set; }

}
