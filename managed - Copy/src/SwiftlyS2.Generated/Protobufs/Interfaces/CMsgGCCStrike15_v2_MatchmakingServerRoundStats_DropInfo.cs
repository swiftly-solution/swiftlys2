
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfo : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfo>
{
  static CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfo ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfoImpl(handle, isManuallyAllocated);


  public uint AccountMvp { get; set; }

}
