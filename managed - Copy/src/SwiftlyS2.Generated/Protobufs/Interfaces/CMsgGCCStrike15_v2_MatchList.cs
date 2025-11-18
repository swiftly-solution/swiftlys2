
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchList : ITypedProtobuf<CMsgGCCStrike15_v2_MatchList>
{
  static CMsgGCCStrike15_v2_MatchList ITypedProtobuf<CMsgGCCStrike15_v2_MatchList>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchListImpl(handle, isManuallyAllocated);


  public uint Msgrequestid { get; set; }


  public uint Accountid { get; set; }


  public uint Servertime { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_MatchInfo> Matches { get; }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> Streams { get; }


  public CDataGCCStrike15_v2_TournamentInfo Tournamentinfo { get; }

}
