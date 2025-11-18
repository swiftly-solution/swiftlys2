
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconItemImpl : TypedProtobuf<CSOEconItem>, CSOEconItem
{
  public CSOEconItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Id
  { get => Accessor.GetUInt64("id"); set => Accessor.SetUInt64("id", value); }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint Inventory
  { get => Accessor.GetUInt32("inventory"); set => Accessor.SetUInt32("inventory", value); }


  public uint DefIndex
  { get => Accessor.GetUInt32("def_index"); set => Accessor.SetUInt32("def_index", value); }


  public uint Quantity
  { get => Accessor.GetUInt32("quantity"); set => Accessor.SetUInt32("quantity", value); }


  public uint Level
  { get => Accessor.GetUInt32("level"); set => Accessor.SetUInt32("level", value); }


  public uint Quality
  { get => Accessor.GetUInt32("quality"); set => Accessor.SetUInt32("quality", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public uint Origin
  { get => Accessor.GetUInt32("origin"); set => Accessor.SetUInt32("origin", value); }


  public string CustomName
  { get => Accessor.GetString("custom_name"); set => Accessor.SetString("custom_name", value); }


  public string CustomDesc
  { get => Accessor.GetString("custom_desc"); set => Accessor.SetString("custom_desc", value); }


  public IProtobufRepeatedFieldSubMessageType<CSOEconItemAttribute> Attribute
  { get => new ProtobufRepeatedFieldSubMessageType<CSOEconItemAttribute>(Accessor, "attribute"); }


  public CSOEconItem InteriorItem
  { get => new CSOEconItemImpl(NativeNetMessages.GetNestedMessage(Address, "interior_item"), false); }


  public bool InUse
  { get => Accessor.GetBool("in_use"); set => Accessor.SetBool("in_use", value); }


  public uint Style
  { get => Accessor.GetUInt32("style"); set => Accessor.SetUInt32("style", value); }


  public ulong OriginalId
  { get => Accessor.GetUInt64("original_id"); set => Accessor.SetUInt64("original_id", value); }


  public IProtobufRepeatedFieldSubMessageType<CSOEconItemEquipped> EquippedState
  { get => new ProtobufRepeatedFieldSubMessageType<CSOEconItemEquipped>(Accessor, "equipped_state"); }


  public uint Rarity
  { get => Accessor.GetUInt32("rarity"); set => Accessor.SetUInt32("rarity", value); }

}
