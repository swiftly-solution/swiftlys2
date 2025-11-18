
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUIFontFilePB : ITypedProtobuf<CUIFontFilePB>
{
  static CUIFontFilePB ITypedProtobuf<CUIFontFilePB>.Wrap(nint handle, bool isManuallyAllocated) => new CUIFontFilePBImpl(handle, isManuallyAllocated);


  public string FontFileName { get; set; }


  public byte[] OpentypeFontData { get; set; }

}
