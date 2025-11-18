
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgApplySticker : ITypedProtobuf<CMsgApplySticker>
{
  static CMsgApplySticker ITypedProtobuf<CMsgApplySticker>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgApplyStickerImpl(handle, isManuallyAllocated);


  public ulong StickerItemId { get; set; }


  public ulong ItemItemId { get; set; }


  public uint StickerSlot { get; set; }


  public uint BaseitemDefidx { get; set; }


  public float StickerWear { get; set; }


  public float StickerRotation { get; set; }


  public float StickerScale { get; set; }


  public float StickerOffsetX { get; set; }


  public float StickerOffsetY { get; set; }


  public float StickerOffsetZ { get; set; }


  public float StickerWearTarget { get; set; }

}
