
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientToGCRequestElevate : ITypedProtobuf<CMsgGCCStrike15_v2_ClientToGCRequestElevate>
{
  static CMsgGCCStrike15_v2_ClientToGCRequestElevate ITypedProtobuf<CMsgGCCStrike15_v2_ClientToGCRequestElevate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientToGCRequestElevateImpl(handle, isManuallyAllocated);


  public uint Stage { get; set; }

}
