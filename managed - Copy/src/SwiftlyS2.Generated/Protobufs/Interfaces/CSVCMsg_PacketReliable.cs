
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_PacketReliable : ITypedProtobuf<CSVCMsg_PacketReliable>, INetMessage<CSVCMsg_PacketReliable>, IDisposable
{
  static int INetMessage<CSVCMsg_PacketReliable>.MessageId => 61;
  
  static string INetMessage<CSVCMsg_PacketReliable>.MessageName => "CSVCMsg_PacketReliable";

  static CSVCMsg_PacketReliable ITypedProtobuf<CSVCMsg_PacketReliable>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PacketReliableImpl(handle, isManuallyAllocated);


  public int Tick { get; set; }


  public int Messagessize { get; set; }


  public bool State { get; set; }

}
