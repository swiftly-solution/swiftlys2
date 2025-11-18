
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_Print : ITypedProtobuf<CSVCMsg_Print>, INetMessage<CSVCMsg_Print>, IDisposable
{
  static int INetMessage<CSVCMsg_Print>.MessageId => 48;
  
  static string INetMessage<CSVCMsg_Print>.MessageName => "CSVCMsg_Print";

  static CSVCMsg_Print ITypedProtobuf<CSVCMsg_Print>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PrintImpl(handle, isManuallyAllocated);


  public string Text { get; set; }

}
