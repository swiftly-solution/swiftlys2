
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_SetView : ITypedProtobuf<CSVCMsg_SetView>, INetMessage<CSVCMsg_SetView>, IDisposable
{
  static int INetMessage<CSVCMsg_SetView>.MessageId => 50;
  
  static string INetMessage<CSVCMsg_SetView>.MessageName => "CSVCMsg_SetView";

  static CSVCMsg_SetView ITypedProtobuf<CSVCMsg_SetView>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_SetViewImpl(handle, isManuallyAllocated);


  public int EntityIndex { get; set; }


  public int Slot { get; set; }

}
