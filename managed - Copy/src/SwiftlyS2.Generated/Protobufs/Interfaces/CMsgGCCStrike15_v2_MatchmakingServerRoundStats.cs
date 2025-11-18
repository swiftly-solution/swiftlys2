
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingServerRoundStats : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerRoundStats>
{
  static CMsgGCCStrike15_v2_MatchmakingServerRoundStats ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerRoundStats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingServerRoundStatsImpl(handle, isManuallyAllocated);


  public ulong Reservationid { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation { get; }


  public string Map { get; set; }


  public int Round { get; set; }


  public IProtobufRepeatedFieldValueType<int> Kills { get; }


  public IProtobufRepeatedFieldValueType<int> Assists { get; }


  public IProtobufRepeatedFieldValueType<int> Deaths { get; }


  public IProtobufRepeatedFieldValueType<int> Scores { get; }


  public IProtobufRepeatedFieldValueType<int> Pings { get; }


  public int RoundResult { get; set; }


  public int MatchResult { get; set; }


  public IProtobufRepeatedFieldValueType<int> TeamScores { get; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerConfirm Confirm { get; }


  public int ReservationStage { get; set; }


  public int MatchDuration { get; set; }


  public IProtobufRepeatedFieldValueType<int> EnemyKills { get; }


  public IProtobufRepeatedFieldValueType<int> EnemyHeadshots { get; }


  public IProtobufRepeatedFieldValueType<int> Enemy3ks { get; }


  public IProtobufRepeatedFieldValueType<int> Enemy4ks { get; }


  public IProtobufRepeatedFieldValueType<int> Enemy5ks { get; }


  public IProtobufRepeatedFieldValueType<int> Mvps { get; }


  public uint SpectatorsCount { get; set; }


  public uint SpectatorsCountTv { get; set; }


  public uint SpectatorsCountLnk { get; set; }


  public IProtobufRepeatedFieldValueType<int> EnemyKillsAgg { get; }


  public CMsgGCCStrike15_v2_MatchmakingServerRoundStats_DropInfo DropInfo { get; }


  public bool BSwitchedTeams { get; set; }


  public IProtobufRepeatedFieldValueType<int> Enemy2ks { get; }


  public IProtobufRepeatedFieldValueType<int> PlayerSpawned { get; }


  public IProtobufRepeatedFieldValueType<int> TeamSpawnCount { get; }


  public uint MaxRounds { get; set; }


  public int MapId { get; set; }

}
