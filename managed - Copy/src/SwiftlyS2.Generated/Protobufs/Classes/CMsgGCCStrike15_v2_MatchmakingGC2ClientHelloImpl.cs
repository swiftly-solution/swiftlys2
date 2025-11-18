
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ClientHelloImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello>, CMsgGCCStrike15_v2_MatchmakingGC2ClientHello
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ClientHelloImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve Ongoingmatch
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl(NativeNetMessages.GetNestedMessage(Address, "ongoingmatch"), false); }


  public GlobalStatistics GlobalStats
  { get => new GlobalStatisticsImpl(NativeNetMessages.GetNestedMessage(Address, "global_stats"), false); }


  public uint PenaltySeconds
  { get => Accessor.GetUInt32("penalty_seconds"); set => Accessor.SetUInt32("penalty_seconds", value); }


  public uint PenaltyReason
  { get => Accessor.GetUInt32("penalty_reason"); set => Accessor.SetUInt32("penalty_reason", value); }


  public int VacBanned
  { get => Accessor.GetInt32("vac_banned"); set => Accessor.SetInt32("vac_banned", value); }


  public PlayerRankingInfo Ranking
  { get => new PlayerRankingInfoImpl(NativeNetMessages.GetNestedMessage(Address, "ranking"), false); }


  public PlayerCommendationInfo Commendation
  { get => new PlayerCommendationInfoImpl(NativeNetMessages.GetNestedMessage(Address, "commendation"), false); }


  public PlayerMedalsInfo Medals
  { get => new PlayerMedalsInfoImpl(NativeNetMessages.GetNestedMessage(Address, "medals"), false); }


  public TournamentEvent MyCurrentEvent
  { get => new TournamentEventImpl(NativeNetMessages.GetNestedMessage(Address, "my_current_event"), false); }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> MyCurrentEventTeams
  { get => new ProtobufRepeatedFieldSubMessageType<TournamentTeam>(Accessor, "my_current_event_teams"); }


  public TournamentTeam MyCurrentTeam
  { get => new TournamentTeamImpl(NativeNetMessages.GetNestedMessage(Address, "my_current_team"), false); }


  public IProtobufRepeatedFieldSubMessageType<TournamentEvent> MyCurrentEventStages
  { get => new ProtobufRepeatedFieldSubMessageType<TournamentEvent>(Accessor, "my_current_event_stages"); }


  public uint SurveyVote
  { get => Accessor.GetUInt32("survey_vote"); set => Accessor.SetUInt32("survey_vote", value); }


  public AccountActivity Activity
  { get => new AccountActivityImpl(NativeNetMessages.GetNestedMessage(Address, "activity"), false); }


  public int PlayerLevel
  { get => Accessor.GetInt32("player_level"); set => Accessor.SetInt32("player_level", value); }


  public int PlayerCurXp
  { get => Accessor.GetInt32("player_cur_xp"); set => Accessor.SetInt32("player_cur_xp", value); }


  public int PlayerXpBonusFlags
  { get => Accessor.GetInt32("player_xp_bonus_flags"); set => Accessor.SetInt32("player_xp_bonus_flags", value); }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo> Rankings
  { get => new ProtobufRepeatedFieldSubMessageType<PlayerRankingInfo>(Accessor, "rankings"); }


  public ulong Owcaseid
  { get => Accessor.GetUInt64("owcaseid"); set => Accessor.SetUInt64("owcaseid", value); }

}
