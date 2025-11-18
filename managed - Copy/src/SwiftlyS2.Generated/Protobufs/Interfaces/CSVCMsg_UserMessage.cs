
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_UserMessage : ITypedProtobuf<CSVCMsg_UserMessage>, INetMessage<CSVCMsg_UserMessage>, IDisposable
{
  static int INetMessage<CSVCMsg_UserMessage>.MessageId => 72;
  
  static string INetMessage<CSVCMsg_UserMessage>.MessageName => "CSVCMsg_UserMessage";

  static CSVCMsg_UserMessage ITypedProtobuf<CSVCMsg_UserMessage>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_UserMessageImpl(handle, isManuallyAllocated);


  public int MsgType { get; set; }


  public byte[] MsgData { get; set; }


  public int Passthrough { get; set; }

}
