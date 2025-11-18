
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerRankingInfoImpl : TypedProtobuf<PlayerRankingInfo>, PlayerRankingInfo
{
  public PlayerRankingInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint RankId
  { get => Accessor.GetUInt32("rank_id"); set => Accessor.SetUInt32("rank_id", value); }


  public uint Wins
  { get => Accessor.GetUInt32("wins"); set => Accessor.SetUInt32("wins", value); }


  public float RankChange
  { get => Accessor.GetFloat("rank_change"); set => Accessor.SetFloat("rank_change", value); }


  public uint RankTypeId
  { get => Accessor.GetUInt32("rank_type_id"); set => Accessor.SetUInt32("rank_type_id", value); }


  public uint TvControl
  { get => Accessor.GetUInt32("tv_control"); set => Accessor.SetUInt32("tv_control", value); }


  public ulong RankWindowStats
  { get => Accessor.GetUInt64("rank_window_stats"); set => Accessor.SetUInt64("rank_window_stats", value); }


  public string LeaderboardName
  { get => Accessor.GetString("leaderboard_name"); set => Accessor.SetString("leaderboard_name", value); }


  public uint RankIfWin
  { get => Accessor.GetUInt32("rank_if_win"); set => Accessor.SetUInt32("rank_if_win", value); }


  public uint RankIfLose
  { get => Accessor.GetUInt32("rank_if_lose"); set => Accessor.SetUInt32("rank_if_lose", value); }


  public uint RankIfTie
  { get => Accessor.GetUInt32("rank_if_tie"); set => Accessor.SetUInt32("rank_if_tie", value); }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo_PerMapRank> PerMapRank
  { get => new ProtobufRepeatedFieldSubMessageType<PlayerRankingInfo_PerMapRank>(Accessor, "per_map_rank"); }


  public uint LeaderboardNameStatus
  { get => Accessor.GetUInt32("leaderboard_name_status"); set => Accessor.SetUInt32("leaderboard_name_status", value); }


  public uint HighestRank
  { get => Accessor.GetUInt32("highest_rank"); set => Accessor.SetUInt32("highest_rank", value); }


  public uint RankExpiry
  { get => Accessor.GetUInt32("rank_expiry"); set => Accessor.SetUInt32("rank_expiry", value); }

}
