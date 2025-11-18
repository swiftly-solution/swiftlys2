
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_UpdateStringTableImpl : NetMessage<CSVCMsg_UpdateStringTable>, CSVCMsg_UpdateStringTable
{
  public CSVCMsg_UpdateStringTableImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int TableId
  { get => Accessor.GetInt32("table_id"); set => Accessor.SetInt32("table_id", value); }


  public int NumChangedEntries
  { get => Accessor.GetInt32("num_changed_entries"); set => Accessor.SetInt32("num_changed_entries", value); }


  public byte[] StringData
  { get => Accessor.GetBytes("string_data"); set => Accessor.SetBytes("string_data", value); }

}
