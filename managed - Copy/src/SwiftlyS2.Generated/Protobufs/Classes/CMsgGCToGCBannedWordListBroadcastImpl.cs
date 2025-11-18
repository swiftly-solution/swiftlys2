
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCBannedWordListBroadcastImpl : TypedProtobuf<CMsgGCToGCBannedWordListBroadcast>, CMsgGCToGCBannedWordListBroadcast
{
  public CMsgGCToGCBannedWordListBroadcastImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CMsgGCBannedWordListResponse Broadcast
  { get => new CMsgGCBannedWordListResponseImpl(NativeNetMessages.GetNestedMessage(Address, "broadcast"), false); }

}
