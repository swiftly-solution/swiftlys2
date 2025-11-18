
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_ClearAllStringTables : ITypedProtobuf<CSVCMsg_ClearAllStringTables>, INetMessage<CSVCMsg_ClearAllStringTables>, IDisposable
{
  static int INetMessage<CSVCMsg_ClearAllStringTables>.MessageId => 51;
  
  static string INetMessage<CSVCMsg_ClearAllStringTables>.MessageName => "CSVCMsg_ClearAllStringTables";

  static CSVCMsg_ClearAllStringTables ITypedProtobuf<CSVCMsg_ClearAllStringTables>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_ClearAllStringTablesImpl(handle, isManuallyAllocated);


  public string Mapname { get; set; }


  public bool CreateTablesSkipped { get; set; }

}
