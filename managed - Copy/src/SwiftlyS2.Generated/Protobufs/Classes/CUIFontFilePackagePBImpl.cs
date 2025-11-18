
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUIFontFilePackagePBImpl : TypedProtobuf<CUIFontFilePackagePB>, CUIFontFilePackagePB
{
  public CUIFontFilePackagePBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint PackageVersion
  { get => Accessor.GetUInt32("package_version"); set => Accessor.SetUInt32("package_version", value); }


  public IProtobufRepeatedFieldSubMessageType<CUIFontFilePackagePB_CUIEncryptedFontFilePB> EncryptedFontFiles
  { get => new ProtobufRepeatedFieldSubMessageType<CUIFontFilePackagePB_CUIEncryptedFontFilePB>(Accessor, "encrypted_font_files"); }

}
