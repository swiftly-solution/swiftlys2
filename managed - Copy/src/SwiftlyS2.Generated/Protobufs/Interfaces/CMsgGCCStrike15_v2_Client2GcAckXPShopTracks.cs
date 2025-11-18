
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Client2GcAckXPShopTracks : ITypedProtobuf<CMsgGCCStrike15_v2_Client2GcAckXPShopTracks>
{
  static CMsgGCCStrike15_v2_Client2GcAckXPShopTracks ITypedProtobuf<CMsgGCCStrike15_v2_Client2GcAckXPShopTracks>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Client2GcAckXPShopTracksImpl(handle, isManuallyAllocated);


}
