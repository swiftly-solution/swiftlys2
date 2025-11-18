
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAdjustEquipSlotImpl : TypedProtobuf<CMsgAdjustEquipSlot>, CMsgAdjustEquipSlot
{
  public CMsgAdjustEquipSlotImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ClassId
  { get => Accessor.GetUInt32("class_id"); set => Accessor.SetUInt32("class_id", value); }


  public uint SlotId
  { get => Accessor.GetUInt32("slot_id"); set => Accessor.SetUInt32("slot_id", value); }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }

}
