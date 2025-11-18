
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgApplyStrangePartImpl : TypedProtobuf<CMsgApplyStrangePart>, CMsgApplyStrangePart
{
  public CMsgApplyStrangePartImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong StrangePartItemId
  { get => Accessor.GetUInt64("strange_part_item_id"); set => Accessor.SetUInt64("strange_part_item_id", value); }


  public ulong ItemItemId
  { get => Accessor.GetUInt64("item_item_id"); set => Accessor.SetUInt64("item_item_id", value); }

}
