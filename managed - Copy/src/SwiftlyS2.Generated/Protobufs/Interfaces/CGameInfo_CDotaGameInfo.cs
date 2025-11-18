
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameInfo_CDotaGameInfo : ITypedProtobuf<CGameInfo_CDotaGameInfo>
{
  static CGameInfo_CDotaGameInfo ITypedProtobuf<CGameInfo_CDotaGameInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CGameInfo_CDotaGameInfoImpl(handle, isManuallyAllocated);


  public ulong MatchId { get; set; }


  public int GameMode { get; set; }


  public int GameWinner { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CGameInfo_CDotaGameInfo_CPlayerInfo> PlayerInfo { get; }


  public uint Leagueid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CGameInfo_CDotaGameInfo_CHeroSelectEvent> PicksBans { get; }


  public uint RadiantTeamId { get; set; }


  public uint DireTeamId { get; set; }


  public string RadiantTeamTag { get; set; }


  public string DireTeamTag { get; set; }


  public uint EndTime { get; set; }

}
