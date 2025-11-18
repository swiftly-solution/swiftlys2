
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class GlobalStatisticsImpl : TypedProtobuf<GlobalStatistics>, GlobalStatistics
{
  public GlobalStatisticsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint PlayersOnline
  { get => Accessor.GetUInt32("players_online"); set => Accessor.SetUInt32("players_online", value); }


  public uint ServersOnline
  { get => Accessor.GetUInt32("servers_online"); set => Accessor.SetUInt32("servers_online", value); }


  public uint PlayersSearching
  { get => Accessor.GetUInt32("players_searching"); set => Accessor.SetUInt32("players_searching", value); }


  public uint ServersAvailable
  { get => Accessor.GetUInt32("servers_available"); set => Accessor.SetUInt32("servers_available", value); }


  public uint OngoingMatches
  { get => Accessor.GetUInt32("ongoing_matches"); set => Accessor.SetUInt32("ongoing_matches", value); }


  public uint SearchTimeAvg
  { get => Accessor.GetUInt32("search_time_avg"); set => Accessor.SetUInt32("search_time_avg", value); }


  public IProtobufRepeatedFieldSubMessageType<DetailedSearchStatistic> SearchStatistics
  { get => new ProtobufRepeatedFieldSubMessageType<DetailedSearchStatistic>(Accessor, "search_statistics"); }


  public string MainPostUrl
  { get => Accessor.GetString("main_post_url"); set => Accessor.SetString("main_post_url", value); }


  public uint RequiredAppidVersion
  { get => Accessor.GetUInt32("required_appid_version"); set => Accessor.SetUInt32("required_appid_version", value); }


  public uint PricesheetVersion
  { get => Accessor.GetUInt32("pricesheet_version"); set => Accessor.SetUInt32("pricesheet_version", value); }


  public uint TwitchStreamsVersion
  { get => Accessor.GetUInt32("twitch_streams_version"); set => Accessor.SetUInt32("twitch_streams_version", value); }


  public uint ActiveTournamentEventid
  { get => Accessor.GetUInt32("active_tournament_eventid"); set => Accessor.SetUInt32("active_tournament_eventid", value); }


  public uint ActiveSurveyId
  { get => Accessor.GetUInt32("active_survey_id"); set => Accessor.SetUInt32("active_survey_id", value); }


  public uint Rtime32Cur
  { get => Accessor.GetUInt32("rtime32_cur"); set => Accessor.SetUInt32("rtime32_cur", value); }


  public uint RequiredAppidVersion2
  { get => Accessor.GetUInt32("required_appid_version2"); set => Accessor.SetUInt32("required_appid_version2", value); }

}
