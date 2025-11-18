
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCDirtyMultipleSDOCache : ITypedProtobuf<CMsgGCToGCDirtyMultipleSDOCache>
{
  static CMsgGCToGCDirtyMultipleSDOCache ITypedProtobuf<CMsgGCToGCDirtyMultipleSDOCache>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCDirtyMultipleSDOCacheImpl(handle, isManuallyAllocated);


  public uint SdoType { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> KeyUint64 { get; }

}
