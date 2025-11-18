
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CProductInfo_SetRichPresenceLocalization_Request_LanguageSection : ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request_LanguageSection>
{
  static CProductInfo_SetRichPresenceLocalization_Request_LanguageSection ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request_LanguageSection>.Wrap(nint handle, bool isManuallyAllocated) => new CProductInfo_SetRichPresenceLocalization_Request_LanguageSectionImpl(handle, isManuallyAllocated);


  public string Language { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CProductInfo_SetRichPresenceLocalization_Request_Token> Tokens { get; }

}
