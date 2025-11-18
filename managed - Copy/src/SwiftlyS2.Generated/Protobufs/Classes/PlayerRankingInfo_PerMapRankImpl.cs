
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerRankingInfo_PerMapRankImpl : TypedProtobuf<PlayerRankingInfo_PerMapRank>, PlayerRankingInfo_PerMapRank
{
  public PlayerRankingInfo_PerMapRankImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint MapId
  { get => Accessor.GetUInt32("map_id"); set => Accessor.SetUInt32("map_id", value); }


  public uint RankId
  { get => Accessor.GetUInt32("rank_id"); set => Accessor.SetUInt32("rank_id", value); }


  public uint Wins
  { get => Accessor.GetUInt32("wins"); set => Accessor.SetUInt32("wins", value); }

}
