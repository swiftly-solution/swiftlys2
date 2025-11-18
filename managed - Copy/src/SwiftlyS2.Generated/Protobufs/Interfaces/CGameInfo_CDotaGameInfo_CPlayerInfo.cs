
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameInfo_CDotaGameInfo_CPlayerInfo : ITypedProtobuf<CGameInfo_CDotaGameInfo_CPlayerInfo>
{
  static CGameInfo_CDotaGameInfo_CPlayerInfo ITypedProtobuf<CGameInfo_CDotaGameInfo_CPlayerInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CGameInfo_CDotaGameInfo_CPlayerInfoImpl(handle, isManuallyAllocated);


  public string HeroName { get; set; }


  public string PlayerName { get; set; }


  public bool IsFakeClient { get; set; }


  public ulong Steamid { get; set; }


  public int GameTeam { get; set; }

}
