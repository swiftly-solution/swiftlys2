
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdate : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdate>
{
  static CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdate ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdateImpl(handle, isManuallyAllocated);


  public string MainPostUrl { get; set; }

}
