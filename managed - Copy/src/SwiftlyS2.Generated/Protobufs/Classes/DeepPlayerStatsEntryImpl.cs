
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class DeepPlayerStatsEntryImpl : TypedProtobuf<DeepPlayerStatsEntry>, DeepPlayerStatsEntry
{
  public DeepPlayerStatsEntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public uint MmGameMode
  { get => Accessor.GetUInt32("mm_game_mode"); set => Accessor.SetUInt32("mm_game_mode", value); }


  public uint Mapid
  { get => Accessor.GetUInt32("mapid"); set => Accessor.SetUInt32("mapid", value); }


  public bool BStartingCt
  { get => Accessor.GetBool("b_starting_ct"); set => Accessor.SetBool("b_starting_ct", value); }


  public uint MatchOutcome
  { get => Accessor.GetUInt32("match_outcome"); set => Accessor.SetUInt32("match_outcome", value); }


  public uint RoundsWon
  { get => Accessor.GetUInt32("rounds_won"); set => Accessor.SetUInt32("rounds_won", value); }


  public uint RoundsLost
  { get => Accessor.GetUInt32("rounds_lost"); set => Accessor.SetUInt32("rounds_lost", value); }


  public uint StatScore
  { get => Accessor.GetUInt32("stat_score"); set => Accessor.SetUInt32("stat_score", value); }


  public uint StatDeaths
  { get => Accessor.GetUInt32("stat_deaths"); set => Accessor.SetUInt32("stat_deaths", value); }


  public uint StatMvps
  { get => Accessor.GetUInt32("stat_mvps"); set => Accessor.SetUInt32("stat_mvps", value); }


  public uint EnemyKills
  { get => Accessor.GetUInt32("enemy_kills"); set => Accessor.SetUInt32("enemy_kills", value); }


  public uint EnemyHeadshots
  { get => Accessor.GetUInt32("enemy_headshots"); set => Accessor.SetUInt32("enemy_headshots", value); }


  public uint Enemy2ks
  { get => Accessor.GetUInt32("enemy_2ks"); set => Accessor.SetUInt32("enemy_2ks", value); }


  public uint Enemy3ks
  { get => Accessor.GetUInt32("enemy_3ks"); set => Accessor.SetUInt32("enemy_3ks", value); }


  public uint Enemy4ks
  { get => Accessor.GetUInt32("enemy_4ks"); set => Accessor.SetUInt32("enemy_4ks", value); }


  public uint TotalDamage
  { get => Accessor.GetUInt32("total_damage"); set => Accessor.SetUInt32("total_damage", value); }


  public uint EngagementsEntryCount
  { get => Accessor.GetUInt32("engagements_entry_count"); set => Accessor.SetUInt32("engagements_entry_count", value); }


  public uint EngagementsEntryWins
  { get => Accessor.GetUInt32("engagements_entry_wins"); set => Accessor.SetUInt32("engagements_entry_wins", value); }


  public uint Engagements1v1Count
  { get => Accessor.GetUInt32("engagements_1v1_count"); set => Accessor.SetUInt32("engagements_1v1_count", value); }


  public uint Engagements1v1Wins
  { get => Accessor.GetUInt32("engagements_1v1_wins"); set => Accessor.SetUInt32("engagements_1v1_wins", value); }


  public uint Engagements1v2Count
  { get => Accessor.GetUInt32("engagements_1v2_count"); set => Accessor.SetUInt32("engagements_1v2_count", value); }


  public uint Engagements1v2Wins
  { get => Accessor.GetUInt32("engagements_1v2_wins"); set => Accessor.SetUInt32("engagements_1v2_wins", value); }


  public uint UtilityCount
  { get => Accessor.GetUInt32("utility_count"); set => Accessor.SetUInt32("utility_count", value); }


  public uint UtilitySuccess
  { get => Accessor.GetUInt32("utility_success"); set => Accessor.SetUInt32("utility_success", value); }


  public uint FlashCount
  { get => Accessor.GetUInt32("flash_count"); set => Accessor.SetUInt32("flash_count", value); }


  public uint FlashSuccess
  { get => Accessor.GetUInt32("flash_success"); set => Accessor.SetUInt32("flash_success", value); }


  public IProtobufRepeatedFieldValueType<uint> Mates
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "mates"); }

}
