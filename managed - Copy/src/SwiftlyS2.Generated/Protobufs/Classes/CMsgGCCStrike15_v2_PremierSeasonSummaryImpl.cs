
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PremierSeasonSummaryImpl : TypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary>, CMsgGCCStrike15_v2_PremierSeasonSummary
{
  public CMsgGCCStrike15_v2_PremierSeasonSummaryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint SeasonId
  { get => Accessor.GetUInt32("season_id"); set => Accessor.SetUInt32("season_id", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek> DataPerWeek
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerWeek>(Accessor, "data_per_week"); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap> DataPerMap
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap>(Accessor, "data_per_map"); }

}
