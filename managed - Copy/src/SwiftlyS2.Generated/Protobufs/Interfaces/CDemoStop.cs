
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoStop : ITypedProtobuf<CDemoStop>
{
  static CDemoStop ITypedProtobuf<CDemoStop>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoStopImpl(handle, isManuallyAllocated);


}
