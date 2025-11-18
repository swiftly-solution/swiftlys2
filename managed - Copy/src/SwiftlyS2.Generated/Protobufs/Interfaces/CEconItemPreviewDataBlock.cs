
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEconItemPreviewDataBlock : ITypedProtobuf<CEconItemPreviewDataBlock>
{
  static CEconItemPreviewDataBlock ITypedProtobuf<CEconItemPreviewDataBlock>.Wrap(nint handle, bool isManuallyAllocated) => new CEconItemPreviewDataBlockImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public ulong Itemid { get; set; }


  public uint Defindex { get; set; }


  public uint Paintindex { get; set; }


  public uint Rarity { get; set; }


  public uint Quality { get; set; }


  public uint Paintwear { get; set; }


  public uint Paintseed { get; set; }


  public uint Killeaterscoretype { get; set; }


  public uint Killeatervalue { get; set; }


  public string Customname { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker> Stickers { get; }


  public uint Inventory { get; set; }


  public uint Origin { get; set; }


  public uint Questid { get; set; }


  public uint Dropreason { get; set; }


  public uint Musicindex { get; set; }


  public int Entindex { get; set; }


  public uint Petindex { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker> Keychains { get; }


  public uint Style { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock_Sticker> Variations { get; }


  public uint UpgradeLevel { get; set; }

}
