
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_ClearAllStringTablesImpl : NetMessage<CSVCMsg_ClearAllStringTables>, CSVCMsg_ClearAllStringTables
{
  public CSVCMsg_ClearAllStringTablesImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Mapname
  { get => Accessor.GetString("mapname"); set => Accessor.SetString("mapname", value); }


  public bool CreateTablesSkipped
  { get => Accessor.GetBool("create_tables_skipped"); set => Accessor.SetBool("create_tables_skipped", value); }

}
