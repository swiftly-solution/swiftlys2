
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUIFontFilePBImpl : TypedProtobuf<CUIFontFilePB>, CUIFontFilePB
{
  public CUIFontFilePBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string FontFileName
  { get => Accessor.GetString("font_file_name"); set => Accessor.SetString("font_file_name", value); }


  public byte[] OpentypeFontData
  { get => Accessor.GetBytes("opentype_font_data"); set => Accessor.SetBytes("opentype_font_data", value); }

}
