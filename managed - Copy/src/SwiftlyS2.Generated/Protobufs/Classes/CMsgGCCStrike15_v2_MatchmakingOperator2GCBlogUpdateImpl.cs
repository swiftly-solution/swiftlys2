
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdateImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdate>, CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdate
{
  public CMsgGCCStrike15_v2_MatchmakingOperator2GCBlogUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string MainPostUrl
  { get => Accessor.GetString("main_post_url"); set => Accessor.SetString("main_post_url", value); }

}
