
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PremierSeasonSummary : ITypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary>
{
  static CMsgGCCStrike15_v2_PremierSeasonSummary ITypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PremierSeasonSummaryImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint SeasonId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek> DataPerWeek { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap> DataPerMap { get; }

}
