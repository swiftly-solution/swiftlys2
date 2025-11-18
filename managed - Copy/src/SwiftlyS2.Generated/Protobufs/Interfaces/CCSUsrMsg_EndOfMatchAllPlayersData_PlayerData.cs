
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData : ITypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData>
{
  static CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData ITypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_EndOfMatchAllPlayersData_PlayerDataImpl(handle, isManuallyAllocated);


  public int Slot { get; set; }


  public ulong Xuid { get; set; }


  public string Name { get; set; }


  public int Teamnumber { get; set; }


  public CCSUsrMsg_EndOfMatchAllPlayersData_Accolade Nomination { get; }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock> Items { get; }


  public int Playercolor { get; set; }


  public bool Isbot { get; set; }

}
