
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_EndOfMatchAllPlayersDataImpl : NetMessage<CCSUsrMsg_EndOfMatchAllPlayersData>, CCSUsrMsg_EndOfMatchAllPlayersData
{
  public CCSUsrMsg_EndOfMatchAllPlayersDataImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData> Allplayerdata
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_EndOfMatchAllPlayersData_PlayerData>(Accessor, "allplayerdata"); }


  public int Scene
  { get => Accessor.GetInt32("scene"); set => Accessor.SetInt32("scene", value); }

}
