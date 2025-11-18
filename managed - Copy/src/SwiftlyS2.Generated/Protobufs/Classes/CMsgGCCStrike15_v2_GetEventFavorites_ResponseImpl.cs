
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GetEventFavorites_ResponseImpl : TypedProtobuf<CMsgGCCStrike15_v2_GetEventFavorites_Response>, CMsgGCCStrike15_v2_GetEventFavorites_Response
{
  public CMsgGCCStrike15_v2_GetEventFavorites_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool AllEvents
  { get => Accessor.GetBool("all_events"); set => Accessor.SetBool("all_events", value); }


  public string JsonFavorites
  { get => Accessor.GetString("json_favorites"); set => Accessor.SetString("json_favorites", value); }


  public string JsonFeatured
  { get => Accessor.GetString("json_featured"); set => Accessor.SetString("json_featured", value); }

}
