
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerRankingInfo_PerMapRank : ITypedProtobuf<PlayerRankingInfo_PerMapRank>
{
  static PlayerRankingInfo_PerMapRank ITypedProtobuf<PlayerRankingInfo_PerMapRank>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerRankingInfo_PerMapRankImpl(handle, isManuallyAllocated);


  public uint MapId { get; set; }


  public uint RankId { get; set; }


  public uint Wins { get; set; }

}
