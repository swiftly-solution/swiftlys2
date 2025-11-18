
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ClientHello : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ClientHello ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ClientHelloImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve Ongoingmatch { get; }


  public GlobalStatistics GlobalStats { get; }


  public uint PenaltySeconds { get; set; }


  public uint PenaltyReason { get; set; }


  public int VacBanned { get; set; }


  public PlayerRankingInfo Ranking { get; }


  public PlayerCommendationInfo Commendation { get; }


  public PlayerMedalsInfo Medals { get; }


  public TournamentEvent MyCurrentEvent { get; }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> MyCurrentEventTeams { get; }


  public TournamentTeam MyCurrentTeam { get; }


  public IProtobufRepeatedFieldSubMessageType<TournamentEvent> MyCurrentEventStages { get; }


  public uint SurveyVote { get; set; }


  public AccountActivity Activity { get; }


  public int PlayerLevel { get; set; }


  public int PlayerCurXp { get; set; }


  public int PlayerXpBonusFlags { get; set; }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo> Rankings { get; }


  public ulong Owcaseid { get; set; }

}
