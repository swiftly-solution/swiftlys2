
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_UpdateStringTable : ITypedProtobuf<CSVCMsg_UpdateStringTable>, INetMessage<CSVCMsg_UpdateStringTable>, IDisposable
{
  static int INetMessage<CSVCMsg_UpdateStringTable>.MessageId => 45;
  
  static string INetMessage<CSVCMsg_UpdateStringTable>.MessageName => "CSVCMsg_UpdateStringTable";

  static CSVCMsg_UpdateStringTable ITypedProtobuf<CSVCMsg_UpdateStringTable>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_UpdateStringTableImpl(handle, isManuallyAllocated);


  public int TableId { get; set; }


  public int NumChangedEntries { get; set; }


  public byte[] StringData { get; set; }

}
