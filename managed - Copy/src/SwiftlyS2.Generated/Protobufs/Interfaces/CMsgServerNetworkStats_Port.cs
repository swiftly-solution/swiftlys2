
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerNetworkStats_Port : ITypedProtobuf<CMsgServerNetworkStats_Port>
{
  static CMsgServerNetworkStats_Port ITypedProtobuf<CMsgServerNetworkStats_Port>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerNetworkStats_PortImpl(handle, isManuallyAllocated);


  public int Port { get; set; }


  public string Name { get; set; }

}
