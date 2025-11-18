
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameInfo_CDotaGameInfoImpl : TypedProtobuf<CGameInfo_CDotaGameInfo>, CGameInfo_CDotaGameInfo
{
  public CGameInfo_CDotaGameInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public int GameMode
  { get => Accessor.GetInt32("game_mode"); set => Accessor.SetInt32("game_mode", value); }


  public int GameWinner
  { get => Accessor.GetInt32("game_winner"); set => Accessor.SetInt32("game_winner", value); }


  public IProtobufRepeatedFieldSubMessageType<CGameInfo_CDotaGameInfo_CPlayerInfo> PlayerInfo
  { get => new ProtobufRepeatedFieldSubMessageType<CGameInfo_CDotaGameInfo_CPlayerInfo>(Accessor, "player_info"); }


  public uint Leagueid
  { get => Accessor.GetUInt32("leagueid"); set => Accessor.SetUInt32("leagueid", value); }


  public IProtobufRepeatedFieldSubMessageType<CGameInfo_CDotaGameInfo_CHeroSelectEvent> PicksBans
  { get => new ProtobufRepeatedFieldSubMessageType<CGameInfo_CDotaGameInfo_CHeroSelectEvent>(Accessor, "picks_bans"); }


  public uint RadiantTeamId
  { get => Accessor.GetUInt32("radiant_team_id"); set => Accessor.SetUInt32("radiant_team_id", value); }


  public uint DireTeamId
  { get => Accessor.GetUInt32("dire_team_id"); set => Accessor.SetUInt32("dire_team_id", value); }


  public string RadiantTeamTag
  { get => Accessor.GetString("radiant_team_tag"); set => Accessor.SetString("radiant_team_tag", value); }


  public string DireTeamTag
  { get => Accessor.GetString("dire_team_tag"); set => Accessor.SetString("dire_team_tag", value); }


  public uint EndTime
  { get => Accessor.GetUInt32("end_time"); set => Accessor.SetUInt32("end_time", value); }

}
