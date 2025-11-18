
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchListRequestTournamentGames : ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestTournamentGames>
{
  static CMsgGCCStrike15_v2_MatchListRequestTournamentGames ITypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestTournamentGames>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchListRequestTournamentGamesImpl(handle, isManuallyAllocated);


  public int Eventid { get; set; }

}
