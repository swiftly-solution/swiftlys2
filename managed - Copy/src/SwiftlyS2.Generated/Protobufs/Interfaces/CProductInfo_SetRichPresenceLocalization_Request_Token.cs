
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CProductInfo_SetRichPresenceLocalization_Request_Token : ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request_Token>
{
  static CProductInfo_SetRichPresenceLocalization_Request_Token ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request_Token>.Wrap(nint handle, bool isManuallyAllocated) => new CProductInfo_SetRichPresenceLocalization_Request_TokenImpl(handle, isManuallyAllocated);


  public string Token { get; set; }


  public string Value { get; set; }

}
