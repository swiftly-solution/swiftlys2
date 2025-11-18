
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface GlobalStatistics : ITypedProtobuf<GlobalStatistics>
{
  static GlobalStatistics ITypedProtobuf<GlobalStatistics>.Wrap(nint handle, bool isManuallyAllocated) => new GlobalStatisticsImpl(handle, isManuallyAllocated);


  public uint PlayersOnline { get; set; }


  public uint ServersOnline { get; set; }


  public uint PlayersSearching { get; set; }


  public uint ServersAvailable { get; set; }


  public uint OngoingMatches { get; set; }


  public uint SearchTimeAvg { get; set; }


  public IProtobufRepeatedFieldSubMessageType<DetailedSearchStatistic> SearchStatistics { get; }


  public string MainPostUrl { get; set; }


  public uint RequiredAppidVersion { get; set; }


  public uint PricesheetVersion { get; set; }


  public uint TwitchStreamsVersion { get; set; }


  public uint ActiveTournamentEventid { get; set; }


  public uint ActiveSurveyId { get; set; }


  public uint Rtime32Cur { get; set; }


  public uint RequiredAppidVersion2 { get; set; }

}
