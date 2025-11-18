
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgDevNewItemRequestImpl : TypedProtobuf<CMsgDevNewItemRequest>, CMsgDevNewItemRequest
{
  public CMsgDevNewItemRequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Receiver
  { get => Accessor.GetUInt64("receiver"); set => Accessor.SetUInt64("receiver", value); }


  public CSOItemCriteria Criteria
  { get => new CSOItemCriteriaImpl(NativeNetMessages.GetNestedMessage(Address, "criteria"), false); }

}
