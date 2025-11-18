
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoStringTablesImpl : TypedProtobuf<CDemoStringTables>, CDemoStringTables
{
  public CDemoStringTablesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CDemoStringTables_table_t> Tables
  { get => new ProtobufRepeatedFieldSubMessageType<CDemoStringTables_table_t>(Accessor, "tables"); }

}
