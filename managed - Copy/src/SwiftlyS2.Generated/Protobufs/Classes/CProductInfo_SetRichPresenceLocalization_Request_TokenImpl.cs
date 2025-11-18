
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CProductInfo_SetRichPresenceLocalization_Request_TokenImpl : TypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request_Token>, CProductInfo_SetRichPresenceLocalization_Request_Token
{
  public CProductInfo_SetRichPresenceLocalization_Request_TokenImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Token
  { get => Accessor.GetString("token"); set => Accessor.SetString("token", value); }


  public string Value
  { get => Accessor.GetString("value"); set => Accessor.SetString("value", value); }

}
