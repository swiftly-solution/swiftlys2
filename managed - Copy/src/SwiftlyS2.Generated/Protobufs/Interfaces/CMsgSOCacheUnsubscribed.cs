
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOCacheUnsubscribed : ITypedProtobuf<CMsgSOCacheUnsubscribed>
{
  static CMsgSOCacheUnsubscribed ITypedProtobuf<CMsgSOCacheUnsubscribed>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOCacheUnsubscribedImpl(handle, isManuallyAllocated);


  public CMsgSOIDOwner OwnerSoid { get; }

}
