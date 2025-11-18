
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap : ITypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap>
{
  static CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap ITypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMapImpl(handle, isManuallyAllocated);


  public uint MapId { get; set; }


  public uint Wins { get; set; }


  public uint Ties { get; set; }


  public uint Losses { get; set; }


  public uint Rounds { get; set; }


  public uint Kills { get; set; }


  public uint Headshots { get; set; }


  public uint Assists { get; set; }


  public uint Deaths { get; set; }


  public uint Mvps { get; set; }


  public uint Rounds3k { get; set; }


  public uint Rounds4k { get; set; }


  public uint Rounds5k { get; set; }

}
