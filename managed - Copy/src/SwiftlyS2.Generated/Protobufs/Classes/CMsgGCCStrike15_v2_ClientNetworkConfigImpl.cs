
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientNetworkConfigImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientNetworkConfig>, CMsgGCCStrike15_v2_ClientNetworkConfig
{
  public CMsgGCCStrike15_v2_ClientNetworkConfigImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
