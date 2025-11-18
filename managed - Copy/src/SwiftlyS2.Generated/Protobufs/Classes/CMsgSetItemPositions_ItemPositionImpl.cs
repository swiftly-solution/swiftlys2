
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSetItemPositions_ItemPositionImpl : TypedProtobuf<CMsgSetItemPositions_ItemPosition>, CMsgSetItemPositions_ItemPosition
{
  public CMsgSetItemPositions_ItemPositionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint LegacyItemId
  { get => Accessor.GetUInt32("legacy_item_id"); set => Accessor.SetUInt32("legacy_item_id", value); }


  public uint Position
  { get => Accessor.GetUInt32("position"); set => Accessor.SetUInt32("position", value); }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }

}
