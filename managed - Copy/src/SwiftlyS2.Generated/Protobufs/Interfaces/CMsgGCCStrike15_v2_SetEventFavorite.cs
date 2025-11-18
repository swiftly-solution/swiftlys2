
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_SetEventFavorite : ITypedProtobuf<CMsgGCCStrike15_v2_SetEventFavorite>
{
  static CMsgGCCStrike15_v2_SetEventFavorite ITypedProtobuf<CMsgGCCStrike15_v2_SetEventFavorite>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_SetEventFavoriteImpl(handle, isManuallyAllocated);


  public ulong Eventid { get; set; }


  public bool IsFavorite { get; set; }

}
