
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch : ITypedProtobuf<CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch>
{
  static CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch ITypedProtobuf<CMsgGCCStrike15_ClientDeepStats_DeepStatsMatch>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_ClientDeepStats_DeepStatsMatchImpl(handle, isManuallyAllocated);


  public DeepPlayerStatsEntry Player { get; }


  public IProtobufRepeatedFieldSubMessageType<DeepPlayerMatchEvent> Events { get; }

}
