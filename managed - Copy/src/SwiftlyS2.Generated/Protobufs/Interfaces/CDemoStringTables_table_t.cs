
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoStringTables_table_t : ITypedProtobuf<CDemoStringTables_table_t>
{
  static CDemoStringTables_table_t ITypedProtobuf<CDemoStringTables_table_t>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoStringTables_table_tImpl(handle, isManuallyAllocated);


  public string TableName { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDemoStringTables_items_t> Items { get; }


  public IProtobufRepeatedFieldSubMessageType<CDemoStringTables_items_t> ItemsClientside { get; }


  public int TableFlags { get; set; }

}
