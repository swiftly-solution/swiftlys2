
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgRequestInventoryRefresh : ITypedProtobuf<CMsgRequestInventoryRefresh>
{
  static CMsgRequestInventoryRefresh ITypedProtobuf<CMsgRequestInventoryRefresh>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgRequestInventoryRefreshImpl(handle, isManuallyAllocated);


}
