
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOCacheUnsubscribedImpl : TypedProtobuf<CMsgSOCacheUnsubscribed>, CMsgSOCacheUnsubscribed
{
  public CMsgSOCacheUnsubscribedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CMsgSOIDOwner OwnerSoid
  { get => new CMsgSOIDOwnerImpl(NativeNetMessages.GetNestedMessage(Address, "owner_soid"), false); }

}
