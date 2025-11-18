
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEconItemPreviewDataBlockImpl : TypedProtobuf<CEconItemPreviewDataBlock>, CEconItemPreviewDataBlock
{
  public CEconItemPreviewDataBlockImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public ulong Itemid
  { get => Accessor.GetUInt64("itemid"); set => Accessor.SetUInt64("itemid", value); }


  public uint Defindex
  { get => Accessor.GetUInt32("defindex"); set => Accessor.SetUInt32("defindex", value); }


  public uint Paintindex
  { get => Accessor.GetUInt32("paintindex"); set => Accessor.SetUInt32("paintindex", value); }


  public uint Rarity
  { get => Accessor.GetUInt32("rarity"); set => Accessor.SetUInt32("rarity", value); }


  public uint Quality
  { get => Accessor.GetUInt32("quality"); set => Accessor.SetUInt32("quality", value); }


  public uint Paintwear
  { get => Accessor.GetUInt32("paintwear"); set => Accessor.SetUInt32("paintwear", value); }


  public uint Paintseed
  { get => Accessor.GetUInt32("paintseed"); set => Accessor.SetUInt32("paintseed", value); }


  public uint Killeaterscoretype
  { get => Accessor.GetUInt32("killeaterscoretype"); set => Accessor.SetUInt32("killeaterscoretype", value); }


  public uint Killeatervalue
  { get => Accessor.GetUInt32("killeatervalue"); set => Accessor.SetUInt32("killeatervalue", value); }


  public string Customname
  { get => Accessor.GetString("customname"); set => Accessor.SetString("customname", value); }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker> Stickers
  { get => new ProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker>(Accessor, "stickers"); }


  public uint Inventory
  { get => Accessor.GetUInt32("inventory"); set => Accessor.SetUInt32("inventory", value); }


  public uint Origin
  { get => Accessor.GetUInt32("origin"); set => Accessor.SetUInt32("origin", value); }


  public uint Questid
  { get => Accessor.GetUInt32("questid"); set => Accessor.SetUInt32("questid", value); }


  public uint Dropreason
  { get => Accessor.GetUInt32("dropreason"); set => Accessor.SetUInt32("dropreason", value); }


  public uint Musicindex
  { get => Accessor.GetUInt32("musicindex"); set => Accessor.SetUInt32("musicindex", value); }


  public int Entindex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }


  public uint Petindex
  { get => Accessor.GetUInt32("petindex"); set => Accessor.SetUInt32("petindex", value); }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker> Keychains
  { get => new ProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker>(Accessor, "keychains"); }


  public uint Style
  { get => Accessor.GetUInt32("style"); set => Accessor.SetUInt32("style", value); }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker> Variations
  { get => new ProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker>(Accessor, "variations"); }


  public uint UpgradeLevel
  { get => Accessor.GetUInt32("upgrade_level"); set => Accessor.SetUInt32("upgrade_level", value); }

}
