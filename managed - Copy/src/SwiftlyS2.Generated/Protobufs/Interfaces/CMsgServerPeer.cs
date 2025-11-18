
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerPeer : ITypedProtobuf<CMsgServerPeer>
{
  static CMsgServerPeer ITypedProtobuf<CMsgServerPeer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerPeerImpl(handle, isManuallyAllocated);


  public int PlayerSlot { get; set; }


  public ulong Steamid { get; set; }


  public CMsgIPCAddress Ipc { get; }


  public bool TheyHearYou { get; set; }


  public bool YouHearThem { get; set; }


  public bool IsListenserverHost { get; set; }

}
