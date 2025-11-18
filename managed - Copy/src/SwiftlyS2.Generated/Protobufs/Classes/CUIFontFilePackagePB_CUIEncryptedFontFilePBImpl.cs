
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUIFontFilePackagePB_CUIEncryptedFontFilePBImpl : TypedProtobuf<CUIFontFilePackagePB_CUIEncryptedFontFilePB>, CUIFontFilePackagePB_CUIEncryptedFontFilePB
{
  public CUIFontFilePackagePB_CUIEncryptedFontFilePBImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public byte[] EncryptedContents
  { get => Accessor.GetBytes("encrypted_contents"); set => Accessor.SetBytes("encrypted_contents", value); }

}
