
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_Broadcast_Command : ITypedProtobuf<CSVCMsg_Broadcast_Command>, INetMessage<CSVCMsg_Broadcast_Command>, IDisposable
{
  static int INetMessage<CSVCMsg_Broadcast_Command>.MessageId => 74;
  
  static string INetMessage<CSVCMsg_Broadcast_Command>.MessageName => "CSVCMsg_Broadcast_Command";

  static CSVCMsg_Broadcast_Command ITypedProtobuf<CSVCMsg_Broadcast_Command>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_Broadcast_CommandImpl(handle, isManuallyAllocated);


  public string Cmd { get; set; }

}
