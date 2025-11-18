
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOCacheSubscribed : ITypedProtobuf<CMsgSOCacheSubscribed>
{
  static CMsgSOCacheSubscribed ITypedProtobuf<CMsgSOCacheSubscribed>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOCacheSubscribedImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheSubscribed_SubscribedType> Objects { get; }


  public ulong Version { get; set; }


  public CMsgSOIDOwner OwnerSoid { get; }

}
