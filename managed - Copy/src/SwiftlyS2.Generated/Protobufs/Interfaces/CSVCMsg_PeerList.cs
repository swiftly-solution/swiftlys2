
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_PeerList : ITypedProtobuf<CSVCMsg_PeerList>, INetMessage<CSVCMsg_PeerList>, IDisposable
{
  static int INetMessage<CSVCMsg_PeerList>.MessageId => 60;
  
  static string INetMessage<CSVCMsg_PeerList>.MessageName => "CSVCMsg_PeerList";

  static CSVCMsg_PeerList ITypedProtobuf<CSVCMsg_PeerList>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PeerListImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgServerPeer> Peer { get; }

}
