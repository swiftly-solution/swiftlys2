
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_GameEvent_key_tImpl : TypedProtobuf<CSVCMsg_GameEvent_key_t>, CSVCMsg_GameEvent_key_t
{
  public CSVCMsg_GameEvent_key_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public string ValString
  { get => Accessor.GetString("val_string"); set => Accessor.SetString("val_string", value); }


  public float ValFloat
  { get => Accessor.GetFloat("val_float"); set => Accessor.SetFloat("val_float", value); }


  public int ValLong
  { get => Accessor.GetInt32("val_long"); set => Accessor.SetInt32("val_long", value); }


  public int ValShort
  { get => Accessor.GetInt32("val_short"); set => Accessor.SetInt32("val_short", value); }


  public int ValByte
  { get => Accessor.GetInt32("val_byte"); set => Accessor.SetInt32("val_byte", value); }


  public bool ValBool
  { get => Accessor.GetBool("val_bool"); set => Accessor.SetBool("val_bool", value); }


  public ulong ValUint64
  { get => Accessor.GetUInt64("val_uint64"); set => Accessor.SetUInt64("val_uint64", value); }

}
