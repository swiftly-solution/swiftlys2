
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientNetworkConfig : ITypedProtobuf<CMsgGCCStrike15_v2_ClientNetworkConfig>
{
  static CMsgGCCStrike15_v2_ClientNetworkConfig ITypedProtobuf<CMsgGCCStrike15_v2_ClientNetworkConfig>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientNetworkConfigImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }

}
