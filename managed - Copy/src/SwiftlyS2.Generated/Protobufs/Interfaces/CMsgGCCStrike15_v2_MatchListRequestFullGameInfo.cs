
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchListRequestFullGameInfo : ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestFullGameInfo>
{
  static CMsgGCCStrike15_v2_MatchListRequestFullGameInfo ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestFullGameInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchListRequestFullGameInfoImpl(handle, isManuallyAllocated);


  public ulong Matchid { get; set; }


  public ulong Outcomeid { get; set; }


  public uint Token { get; set; }

}
