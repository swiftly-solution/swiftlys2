
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoSyncTick : ITypedProtobuf<CDemoSyncTick>
{
  static CDemoSyncTick ITypedProtobuf<CDemoSyncTick>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoSyncTickImpl(handle, isManuallyAllocated);


}
