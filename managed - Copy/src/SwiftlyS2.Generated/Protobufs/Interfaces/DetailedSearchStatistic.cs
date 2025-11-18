
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface DetailedSearchStatistic : ITypedProtobuf<DetailedSearchStatistic>
{
  static DetailedSearchStatistic ITypedProtobuf<DetailedSearchStatistic>.Wrap(nint handle, bool isManuallyAllocated) => new DetailedSearchStatisticImpl(handle, isManuallyAllocated);


  public uint GameType { get; set; }


  public uint SearchTimeAvg { get; set; }


  public uint PlayersSearching { get; set; }

}
