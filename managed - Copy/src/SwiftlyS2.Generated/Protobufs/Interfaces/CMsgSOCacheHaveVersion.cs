
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOCacheHaveVersion : ITypedProtobuf<CMsgSOCacheHaveVersion>
{
  static CMsgSOCacheHaveVersion ITypedProtobuf<CMsgSOCacheHaveVersion>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOCacheHaveVersionImpl(handle, isManuallyAllocated);


  public CMsgSOIDOwner Soid { get; }


  public ulong Version { get; set; }

}
