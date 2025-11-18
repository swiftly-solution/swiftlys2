
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientVarValueNotificationInfoImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientVarValueNotificationInfo>, CMsgGCCStrike15_v2_ClientVarValueNotificationInfo
{
  public CMsgGCCStrike15_v2_ClientVarValueNotificationInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string ValueName
  { get => Accessor.GetString("value_name"); set => Accessor.SetString("value_name", value); }


  public int ValueInt
  { get => Accessor.GetInt32("value_int"); set => Accessor.SetInt32("value_int", value); }


  public uint ServerAddr
  { get => Accessor.GetUInt32("server_addr"); set => Accessor.SetUInt32("server_addr", value); }


  public uint ServerPort
  { get => Accessor.GetUInt32("server_port"); set => Accessor.SetUInt32("server_port", value); }


  public IProtobufRepeatedFieldValueType<string> ChokedBlocks
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "choked_blocks"); }

}
