
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCWebAPIAccountChanged : ITypedProtobuf<CMsgGCToGCWebAPIAccountChanged>
{
  static CMsgGCToGCWebAPIAccountChanged ITypedProtobuf<CMsgGCToGCWebAPIAccountChanged>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCWebAPIAccountChangedImpl(handle, isManuallyAllocated);


}
