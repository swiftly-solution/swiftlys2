
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GetEventFavorites_Request : ITypedProtobuf<CMsgGCCStrike15_v2_GetEventFavorites_Request>
{
  static CMsgGCCStrike15_v2_GetEventFavorites_Request ITypedProtobuf<CMsgGCCStrike15_v2_GetEventFavorites_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GetEventFavorites_RequestImpl(handle, isManuallyAllocated);


  public bool AllEvents { get; set; }

}
