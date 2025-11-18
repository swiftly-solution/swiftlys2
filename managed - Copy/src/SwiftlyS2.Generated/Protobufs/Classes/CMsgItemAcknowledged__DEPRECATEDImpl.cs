
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgItemAcknowledged__DEPRECATEDImpl : TypedProtobuf<CMsgItemAcknowledged__DEPRECATED>, CMsgItemAcknowledged__DEPRECATED
{
  public CMsgItemAcknowledged__DEPRECATEDImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint Inventory
  { get => Accessor.GetUInt32("inventory"); set => Accessor.SetUInt32("inventory", value); }


  public uint DefIndex
  { get => Accessor.GetUInt32("def_index"); set => Accessor.SetUInt32("def_index", value); }


  public uint Quality
  { get => Accessor.GetUInt32("quality"); set => Accessor.SetUInt32("quality", value); }


  public uint Rarity
  { get => Accessor.GetUInt32("rarity"); set => Accessor.SetUInt32("rarity", value); }


  public uint Origin
  { get => Accessor.GetUInt32("origin"); set => Accessor.SetUInt32("origin", value); }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }

}
