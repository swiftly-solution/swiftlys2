
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_GetCvarValue : ITypedProtobuf<CSVCMsg_GetCvarValue>, INetMessage<CSVCMsg_GetCvarValue>, IDisposable
{
  static int INetMessage<CSVCMsg_GetCvarValue>.MessageId => 58;
  
  static string INetMessage<CSVCMsg_GetCvarValue>.MessageName => "CSVCMsg_GetCvarValue";

  static CSVCMsg_GetCvarValue ITypedProtobuf<CSVCMsg_GetCvarValue>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_GetCvarValueImpl(handle, isManuallyAllocated);


  public int Cookie { get; set; }


  public string CvarName { get; set; }

}
