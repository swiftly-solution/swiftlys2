
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_ClientDeepStats : ITypedProtobuf<CMsgGCCStrike15_ClientDeepStats>
{
  static CMsgGCCStrike15_ClientDeepStats ITypedProtobuf<CMsgGCCStrike15_ClientDeepStats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_ClientDeepStatsImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public CMsgGCCStrike15_ClientDeepStats_DeepStatsRange Range { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch> Matches { get; }

}
