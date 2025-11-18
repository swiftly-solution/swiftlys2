
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEconItemPreviewDataBlock_StickerImpl : TypedProtobuf<CEconItemPreviewDataBlock_Sticker>, CEconItemPreviewDataBlock_Sticker
{
  public CEconItemPreviewDataBlock_StickerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Slot
  { get => Accessor.GetUInt32("slot"); set => Accessor.SetUInt32("slot", value); }


  public uint StickerId
  { get => Accessor.GetUInt32("sticker_id"); set => Accessor.SetUInt32("sticker_id", value); }


  public float Wear
  { get => Accessor.GetFloat("wear"); set => Accessor.SetFloat("wear", value); }


  public float Scale
  { get => Accessor.GetFloat("scale"); set => Accessor.SetFloat("scale", value); }


  public float Rotation
  { get => Accessor.GetFloat("rotation"); set => Accessor.SetFloat("rotation", value); }


  public uint TintId
  { get => Accessor.GetUInt32("tint_id"); set => Accessor.SetUInt32("tint_id", value); }


  public float OffsetX
  { get => Accessor.GetFloat("offset_x"); set => Accessor.SetFloat("offset_x", value); }


  public float OffsetY
  { get => Accessor.GetFloat("offset_y"); set => Accessor.SetFloat("offset_y", value); }


  public float OffsetZ
  { get => Accessor.GetFloat("offset_z"); set => Accessor.SetFloat("offset_z", value); }


  public uint Pattern
  { get => Accessor.GetUInt32("pattern"); set => Accessor.SetUInt32("pattern", value); }


  public uint HighlightReel
  { get => Accessor.GetUInt32("highlight_reel"); set => Accessor.SetUInt32("highlight_reel", value); }


  public uint WrappedSticker
  { get => Accessor.GetUInt32("wrapped_sticker"); set => Accessor.SetUInt32("wrapped_sticker", value); }

}
