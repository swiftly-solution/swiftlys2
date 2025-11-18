
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerAvailable : ITypedProtobuf<CMsgServerAvailable>
{
  static CMsgServerAvailable ITypedProtobuf<CMsgServerAvailable>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerAvailableImpl(handle, isManuallyAllocated);


}
