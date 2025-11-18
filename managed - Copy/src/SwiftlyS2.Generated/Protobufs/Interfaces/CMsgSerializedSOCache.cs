
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSerializedSOCache : ITypedProtobuf<CMsgSerializedSOCache>
{
  static CMsgSerializedSOCache ITypedProtobuf<CMsgSerializedSOCache>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSerializedSOCacheImpl(handle, isManuallyAllocated);


  public uint FileVersion { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSerializedSOCache_Cache> Caches { get; }


  public uint GcSocacheFileVersion { get; set; }

}
