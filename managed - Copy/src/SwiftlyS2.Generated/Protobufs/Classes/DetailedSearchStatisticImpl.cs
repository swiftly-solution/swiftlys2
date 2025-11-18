
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class DetailedSearchStatisticImpl : TypedProtobuf<DetailedSearchStatistic>, DetailedSearchStatistic
{
  public DetailedSearchStatisticImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public uint SearchTimeAvg
  { get => Accessor.GetUInt32("search_time_avg"); set => Accessor.SetUInt32("search_time_avg", value); }


  public uint PlayersSearching
  { get => Accessor.GetUInt32("players_searching"); set => Accessor.SetUInt32("players_searching", value); }

}
