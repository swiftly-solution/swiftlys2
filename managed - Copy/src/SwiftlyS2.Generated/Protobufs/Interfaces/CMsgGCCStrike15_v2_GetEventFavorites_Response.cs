
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GetEventFavorites_Response : ITypedProtobuf<CMsgGCCStrike15_v2_GetEventFavorites_Response>
{
  static CMsgGCCStrike15_v2_GetEventFavorites_Response ITypedProtobuf<CMsgGCCStrike15_v2_GetEventFavorites_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GetEventFavorites_ResponseImpl(handle, isManuallyAllocated);


  public bool AllEvents { get; set; }


  public string JsonFavorites { get; set; }


  public string JsonFeatured { get; set; }

}
