
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface C2S_CONNECTION_Message : ITypedProtobuf<C2S_CONNECTION_Message>
{
  static C2S_CONNECTION_Message ITypedProtobuf<C2S_CONNECTION_Message>.Wrap(nint handle, bool isManuallyAllocated) => new C2S_CONNECTION_MessageImpl(handle, isManuallyAllocated);


  public string AddonName { get; set; }


  public C2S_CONNECT_SameProcessCheck LocalhostSameProcessCheck { get; }

}
