
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSerializedSOCache_TypeCache : ITypedProtobuf<CMsgSerializedSOCache_TypeCache>
{
  static CMsgSerializedSOCache_TypeCache ITypedProtobuf<CMsgSerializedSOCache_TypeCache>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSerializedSOCache_TypeCacheImpl(handle, isManuallyAllocated);


  public uint Type { get; set; }


  public IProtobufRepeatedFieldValueType<byte[]> Objects { get; }


  public uint ServiceId { get; set; }

}
