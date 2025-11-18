
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CProductInfo_SetRichPresenceLocalization_Request : ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request>
{
  static CProductInfo_SetRichPresenceLocalization_Request ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CProductInfo_SetRichPresenceLocalization_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CProductInfo_SetRichPresenceLocalization_Request_LanguageSection> Languages { get; }


  public ulong Steamid { get; set; }

}
