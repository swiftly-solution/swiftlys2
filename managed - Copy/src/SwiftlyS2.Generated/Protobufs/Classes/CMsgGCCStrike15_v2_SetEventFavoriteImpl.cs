
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_SetEventFavoriteImpl : TypedProtobuf<CMsgGCCStrike15_v2_SetEventFavorite>, CMsgGCCStrike15_v2_SetEventFavorite
{
  public CMsgGCCStrike15_v2_SetEventFavoriteImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Eventid
  { get => Accessor.GetUInt64("eventid"); set => Accessor.SetUInt64("eventid", value); }


  public bool IsFavorite
  { get => Accessor.GetBool("is_favorite"); set => Accessor.SetBool("is_favorite", value); }

}
