
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameInfo_CDotaGameInfo_CPlayerInfoImpl : TypedProtobuf<CGameInfo_CDotaGameInfo_CPlayerInfo>, CGameInfo_CDotaGameInfo_CPlayerInfo
{
  public CGameInfo_CDotaGameInfo_CPlayerInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string HeroName
  { get => Accessor.GetString("hero_name"); set => Accessor.SetString("hero_name", value); }


  public string PlayerName
  { get => Accessor.GetString("player_name"); set => Accessor.SetString("player_name", value); }


  public bool IsFakeClient
  { get => Accessor.GetBool("is_fake_client"); set => Accessor.SetBool("is_fake_client", value); }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public int GameTeam
  { get => Accessor.GetInt32("game_team"); set => Accessor.SetInt32("game_team", value); }

}
