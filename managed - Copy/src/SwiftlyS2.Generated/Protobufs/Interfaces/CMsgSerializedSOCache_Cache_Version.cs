
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSerializedSOCache_Cache_Version : ITypedProtobuf<CMsgSerializedSOCache_Cache_Version>
{
  static CMsgSerializedSOCache_Cache_Version ITypedProtobuf<CMsgSerializedSOCache_Cache_Version>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSerializedSOCache_Cache_VersionImpl(handle, isManuallyAllocated);


  public uint Service { get; set; }


  public ulong Version { get; set; }

}
