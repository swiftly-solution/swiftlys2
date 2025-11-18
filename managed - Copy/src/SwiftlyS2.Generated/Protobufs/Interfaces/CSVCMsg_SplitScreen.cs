
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_SplitScreen : ITypedProtobuf<CSVCMsg_SplitScreen>, INetMessage<CSVCMsg_SplitScreen>, IDisposable
{
  static int INetMessage<CSVCMsg_SplitScreen>.MessageId => 54;
  
  static string INetMessage<CSVCMsg_SplitScreen>.MessageName => "CSVCMsg_SplitScreen";

  static CSVCMsg_SplitScreen ITypedProtobuf<CSVCMsg_SplitScreen>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_SplitScreenImpl(handle, isManuallyAllocated);


  public ESplitScreenMessageType Type { get; set; }


  public int Slot { get; set; }


  public int PlayerIndex { get; set; }

}
