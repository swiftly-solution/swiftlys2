
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_ClientDeepStats_DeepStatsRange : ITypedProtobuf<CMsgGCCStrike15_ClientDeepStats_DeepStatsRange>
{
  static CMsgGCCStrike15_ClientDeepStats_DeepStatsRange ITypedProtobuf<CMsgGCCStrike15_ClientDeepStats_DeepStatsRange>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_ClientDeepStats_DeepStatsRangeImpl(handle, isManuallyAllocated);


  public uint Begin { get; set; }


  public uint End { get; set; }


  public bool Frozen { get; set; }

}
