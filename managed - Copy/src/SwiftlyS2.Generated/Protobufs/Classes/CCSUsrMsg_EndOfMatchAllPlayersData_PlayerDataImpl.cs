
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_EndOfMatchAllPlayersData_PlayerDataImpl : TypedProtobuf<CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData>, CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData
{
  public CCSUsrMsg_EndOfMatchAllPlayersData_PlayerDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Slot
  { get => Accessor.GetInt32("slot"); set => Accessor.SetInt32("slot", value); }


  public ulong Xuid
  { get => Accessor.GetUInt64("xuid"); set => Accessor.SetUInt64("xuid", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public int Teamnumber
  { get => Accessor.GetInt32("teamnumber"); set => Accessor.SetInt32("teamnumber", value); }


  public CCSUsrMsg_EndOfMatchAllPlayersData_Accolade Nomination
  { get => new CCSUsrMsg_EndOfMatchAllPlayersData_AccoladeImpl(NativeNetMessages.GetNestedMessage(Address, "nomination"), false); }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock> Items
  { get => new ProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock>(Accessor, "items"); }


  public int Playercolor
  { get => Accessor.GetInt32("playercolor"); set => Accessor.SetInt32("playercolor", value); }


  public bool Isbot
  { get => Accessor.GetBool("isbot"); set => Accessor.SetBool("isbot", value); }

}
