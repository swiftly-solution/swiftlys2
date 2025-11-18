
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirmImpl(handle, isManuallyAllocated);


  public uint Token { get; set; }


  public uint Stamp { get; set; }


  public ulong Exchange { get; set; }


  public uint Retry { get; set; }

}
