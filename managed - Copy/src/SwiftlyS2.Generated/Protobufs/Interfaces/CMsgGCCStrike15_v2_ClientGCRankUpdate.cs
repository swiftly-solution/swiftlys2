
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientGCRankUpdate : ITypedProtobuf<CMsgGCCStrike15_v2_ClientGCRankUpdate>
{
  static CMsgGCCStrike15_v2_ClientGCRankUpdate ITypedProtobuf<CMsgGCCStrike15_v2_ClientGCRankUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientGCRankUpdateImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo> Rankings { get; }

}
