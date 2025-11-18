
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconEquipSlotImpl : TypedProtobuf<CSOEconEquipSlot>, CSOEconEquipSlot
{
  public CSOEconEquipSlotImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint ClassId
  { get => Accessor.GetUInt32("class_id"); set => Accessor.SetUInt32("class_id", value); }


  public uint SlotId
  { get => Accessor.GetUInt32("slot_id"); set => Accessor.SetUInt32("slot_id", value); }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }


  public uint ItemDefinition
  { get => Accessor.GetUInt32("item_definition"); set => Accessor.SetUInt32("item_definition", value); }

}
