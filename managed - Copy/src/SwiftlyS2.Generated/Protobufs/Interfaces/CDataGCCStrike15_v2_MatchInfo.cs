
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_MatchInfo : ITypedProtobuf<CDataGCCStrike15_v2_MatchInfo>
{
  static CDataGCCStrike15_v2_MatchInfo ITypedProtobuf<CDataGCCStrike15_v2_MatchInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_MatchInfoImpl(handle, isManuallyAllocated);


  public ulong Matchid { get; set; }


  public uint Matchtime { get; set; }


  public WatchableMatchInfo Watchablematchinfo { get; }


  public CMsgGCCStrike15_v2_MatchmakingServerRoundStats RoundstatsLegacy { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingServerRoundStats> Roundstatsall { get; }

}
