
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_PlayerStatsUpdateImpl : NetMessage<CCSUsrMsg_PlayerStatsUpdate>, CCSUsrMsg_PlayerStatsUpdate
{
  public CCSUsrMsg_PlayerStatsUpdateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Version
  { get => Accessor.GetInt32("version"); set => Accessor.SetInt32("version", value); }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_PlayerStatsUpdate_Stat> Stats
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_PlayerStatsUpdate_Stat>(Accessor, "stats"); }


  public uint Ehandle
  { get => Accessor.GetUInt32("ehandle"); set => Accessor.SetUInt32("ehandle", value); }


  public int Crc
  { get => Accessor.GetInt32("crc"); set => Accessor.SetInt32("crc", value); }

}
