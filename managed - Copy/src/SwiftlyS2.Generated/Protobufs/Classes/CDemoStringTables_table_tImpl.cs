
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoStringTables_table_tImpl : TypedProtobuf<CDemoStringTables_table_t>, CDemoStringTables_table_t
{
  public CDemoStringTables_table_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string TableName
  { get => Accessor.GetString("table_name"); set => Accessor.SetString("table_name", value); }


  public IProtobufRepeatedFieldSubMessageType<CDemoStringTables_items_t> Items
  { get => new ProtobufRepeatedFieldSubMessageType<CDemoStringTables_items_t>(Accessor, "items"); }


  public IProtobufRepeatedFieldSubMessageType<CDemoStringTables_items_t> ItemsClientside
  { get => new ProtobufRepeatedFieldSubMessageType<CDemoStringTables_items_t>(Accessor, "items_clientside"); }


  public int TableFlags
  { get => Accessor.GetInt32("table_flags"); set => Accessor.SetInt32("table_flags", value); }

}
