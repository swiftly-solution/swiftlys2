
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgApplyStatTrakSwapImpl : TypedProtobuf<CMsgApplyStatTrakSwap>, CMsgApplyStatTrakSwap
{
  public CMsgApplyStatTrakSwapImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ToolItemId
  { get => Accessor.GetUInt64("tool_item_id"); set => Accessor.SetUInt64("tool_item_id", value); }


  public ulong Item1ItemId
  { get => Accessor.GetUInt64("item_1_item_id"); set => Accessor.SetUInt64("item_1_item_id", value); }


  public ulong Item2ItemId
  { get => Accessor.GetUInt64("item_2_item_id"); set => Accessor.SetUInt64("item_2_item_id", value); }

}
