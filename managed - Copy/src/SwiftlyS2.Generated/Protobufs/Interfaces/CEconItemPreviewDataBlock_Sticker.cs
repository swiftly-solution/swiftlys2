
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEconItemPreviewDataBlock_Sticker : ITypedProtobuf<CEconItemPreviewDataBlock_Sticker>
{
  static CEconItemPreviewDataBlock_Sticker ITypedProtobuf<CEconItemPreviewDataBlock_Sticker>.Wrap(nint handle, bool isManuallyAllocated) => new CEconItemPreviewDataBlock_StickerImpl(handle, isManuallyAllocated);


  public uint Slot { get; set; }


  public uint StickerId { get; set; }


  public float Wear { get; set; }


  public float Scale { get; set; }


  public float Rotation { get; set; }


  public uint TintId { get; set; }


  public float OffsetX { get; set; }


  public float OffsetY { get; set; }


  public float OffsetZ { get; set; }


  public uint Pattern { get; set; }


  public uint HighlightReel { get; set; }


  public uint WrappedSticker { get; set; }

}
