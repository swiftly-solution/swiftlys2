
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GetEventFavorites_RequestImpl : TypedProtobuf<CMsgGCCStrike15_v2_GetEventFavorites_Request>, CMsgGCCStrike15_v2_GetEventFavorites_Request
{
  public CMsgGCCStrike15_v2_GetEventFavorites_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool AllEvents
  { get => Accessor.GetBool("all_events"); set => Accessor.SetBool("all_events", value); }

}
