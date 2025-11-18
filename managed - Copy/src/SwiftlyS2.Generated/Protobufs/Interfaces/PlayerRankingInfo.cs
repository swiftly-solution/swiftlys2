
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerRankingInfo : ITypedProtobuf<PlayerRankingInfo>
{
  static PlayerRankingInfo ITypedProtobuf<PlayerRankingInfo>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerRankingInfoImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint RankId { get; set; }


  public uint Wins { get; set; }


  public float RankChange { get; set; }


  public uint RankTypeId { get; set; }


  public uint TvControl { get; set; }


  public ulong RankWindowStats { get; set; }


  public string LeaderboardName { get; set; }


  public uint RankIfWin { get; set; }


  public uint RankIfLose { get; set; }


  public uint RankIfTie { get; set; }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo_PerMapRank> PerMapRank { get; }


  public uint LeaderboardNameStatus { get; set; }


  public uint HighestRank { get; set; }


  public uint RankExpiry { get; set; }

}
