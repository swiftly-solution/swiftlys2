
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgCasketItemImpl : TypedProtobuf<CMsgCasketItem>, CMsgCasketItem
{
  public CMsgCasketItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong CasketItemId
  { get => Accessor.GetUInt64("casket_item_id"); set => Accessor.SetUInt64("casket_item_id", value); }


  public ulong ItemItemId
  { get => Accessor.GetUInt64("item_item_id"); set => Accessor.SetUInt64("item_item_id", value); }

}
