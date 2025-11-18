
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CProductInfo_SetRichPresenceLocalization_Request_LanguageSectionImpl : TypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request_LanguageSection>, CProductInfo_SetRichPresenceLocalization_Request_LanguageSection
{
  public CProductInfo_SetRichPresenceLocalization_Request_LanguageSectionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Language
  { get => Accessor.GetString("language"); set => Accessor.SetString("language", value); }


  public IProtobufRepeatedFieldSubMessageType<CProductInfo_SetRichPresenceLocalization_Request_Token> Tokens
  { get => new ProtobufRepeatedFieldSubMessageType<CProductInfo_SetRichPresenceLocalization_Request_Token>(Accessor, "tokens"); }

}
