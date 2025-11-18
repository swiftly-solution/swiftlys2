
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchListRequestTournamentGamesImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestTournamentGames>, CMsgGCCStrike15_v2_MatchListRequestTournamentGames
{
  public CMsgGCCStrike15_v2_MatchListRequestTournamentGamesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }

}
