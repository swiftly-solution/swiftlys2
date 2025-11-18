
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingServerRoundStatsImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerRoundStats>, CMsgGCCStrike15_v2_MatchmakingServerRoundStats
{
  public CMsgGCCStrike15_v2_MatchmakingServerRoundStatsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Reservationid
  { get => Accessor.GetUInt64("reservationid"); set => Accessor.SetUInt64("reservationid", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl(NativeNetMessages.GetNestedMessage(Address, "reservation"), false); }


  public string Map
  { get => Accessor.GetString("map"); set => Accessor.SetString("map", value); }


  public int Round
  { get => Accessor.GetInt32("round"); set => Accessor.SetInt32("round", value); }


  public IProtobufRepeatedFieldValueType<int> Kills
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "kills"); }


  public IProtobufRepeatedFieldValueType<int> Assists
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "assists"); }


  public IProtobufRepeatedFieldValueType<int> Deaths
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "deaths"); }


  public IProtobufRepeatedFieldValueType<int> Scores
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "scores"); }


  public IProtobufRepeatedFieldValueType<int> Pings
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "pings"); }


  public int RoundResult
  { get => Accessor.GetInt32("round_result"); set => Accessor.SetInt32("round_result", value); }


  public int MatchResult
  { get => Accessor.GetInt32("match_result"); set => Accessor.SetInt32("match_result", value); }


  public IProtobufRepeatedFieldValueType<int> TeamScores
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "team_scores"); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm Confirm
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirmImpl(NativeNetMessages.GetNestedMessage(Address, "confirm"), false); }


  public int ReservationStage
  { get => Accessor.GetInt32("reservation_stage"); set => Accessor.SetInt32("reservation_stage", value); }


  public int MatchDuration
  { get => Accessor.GetInt32("match_duration"); set => Accessor.SetInt32("match_duration", value); }


  public IProtobufRepeatedFieldValueType<int> EnemyKills
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_kills"); }


  public IProtobufRepeatedFieldValueType<int> EnemyHeadshots
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_headshots"); }


  public IProtobufRepeatedFieldValueType<int> Enemy3ks
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_3ks"); }


  public IProtobufRepeatedFieldValueType<int> Enemy4ks
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_4ks"); }


  public IProtobufRepeatedFieldValueType<int> Enemy5ks
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_5ks"); }


  public IProtobufRepeatedFieldValueType<int> Mvps
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "mvps"); }


  public uint SpectatorsCount
  { get => Accessor.GetUInt32("spectators_count"); set => Accessor.SetUInt32("spectators_count", value); }


  public uint SpectatorsCountTv
  { get => Accessor.GetUInt32("spectators_count_tv"); set => Accessor.SetUInt32("spectators_count_tv", value); }


  public uint SpectatorsCountLnk
  { get => Accessor.GetUInt32("spectators_count_lnk"); set => Accessor.SetUInt32("spectators_count_lnk", value); }


  public IProtobufRepeatedFieldValueType<int> EnemyKillsAgg
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_kills_agg"); }


  public CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfo DropInfo
  { get => new CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfoImpl(NativeNetMessages.GetNestedMessage(Address, "drop_info"), false); }


  public bool BSwitchedTeams
  { get => Accessor.GetBool("b_switched_teams"); set => Accessor.SetBool("b_switched_teams", value); }


  public IProtobufRepeatedFieldValueType<int> Enemy2ks
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "enemy_2ks"); }


  public IProtobufRepeatedFieldValueType<int> PlayerSpawned
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "player_spawned"); }


  public IProtobufRepeatedFieldValueType<int> TeamSpawnCount
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "team_spawn_count"); }


  public uint MaxRounds
  { get => Accessor.GetUInt32("max_rounds"); set => Accessor.SetUInt32("max_rounds", value); }


  public int MapId
  { get => Accessor.GetInt32("map_id"); set => Accessor.SetInt32("map_id", value); }

}
