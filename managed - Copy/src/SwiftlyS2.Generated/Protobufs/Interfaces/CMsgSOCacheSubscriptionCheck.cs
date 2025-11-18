
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOCacheSubscriptionCheck : ITypedProtobuf<CMsgSOCacheSubscriptionCheck>
{
  static CMsgSOCacheSubscriptionCheck ITypedProtobuf<CMsgSOCacheSubscriptionCheck>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOCacheSubscriptionCheckImpl(handle, isManuallyAllocated);


  public ulong Version { get; set; }


  public CMsgSOIDOwner OwnerSoid { get; }

}
