
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingStopImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingStop>, CMsgGCCStrike15_v2_MatchmakingStop
{
  public CMsgGCCStrike15_v2_MatchmakingStopImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Abandon
  { get => Accessor.GetInt32("abandon"); set => Accessor.SetInt32("abandon", value); }

}
