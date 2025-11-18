
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PeerListImpl : NetMessage<CSVCMsg_PeerList>, CSVCMsg_PeerList
{
  public CSVCMsg_PeerListImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgServerPeer> Peer
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgServerPeer>(Accessor, "peer"); }

}
