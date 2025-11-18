
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSDONoMemcached : ITypedProtobuf<CMsgSDONoMemcached>
{
  static CMsgSDONoMemcached ITypedProtobuf<CMsgSDONoMemcached>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSDONoMemcachedImpl(handle, isManuallyAllocated);


}
