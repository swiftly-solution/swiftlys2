
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOCacheSubscribed_SubscribedType : ITypedProtobuf<CMsgSOCacheSubscribed_SubscribedType>
{
  static CMsgSOCacheSubscribed_SubscribedType ITypedProtobuf<CMsgSOCacheSubscribed_SubscribedType>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOCacheSubscribed_SubscribedTypeImpl(handle, isManuallyAllocated);


  public int TypeId { get; set; }


  public IProtobufRepeatedFieldValueType<byte[]> ObjectData { get; }

}
