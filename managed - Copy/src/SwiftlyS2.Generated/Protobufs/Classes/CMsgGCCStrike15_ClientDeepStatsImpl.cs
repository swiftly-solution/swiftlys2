
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_ClientDeepStatsImpl : TypedProtobuf<CMsgGCCStrike15_ClientDeepStats>, CMsgGCCStrike15_ClientDeepStats
{
  public CMsgGCCStrike15_ClientDeepStatsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public CMsgGCCStrike15_ClientDeepStats_DeepStatsRange Range
  { get => new CMsgGCCStrike15_ClientDeepStats_DeepStatsRangeImpl(NativeNetMessages.GetNestedMessage(Address, "range"), false); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch> Matches
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch>(Accessor, "matches"); }

}
