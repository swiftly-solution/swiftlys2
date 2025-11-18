
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CProductInfo_SetRichPresenceLocalization_Response : ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Response>
{
  static CProductInfo_SetRichPresenceLocalization_Response ITypedProtobuf<CProductInfo_SetRichPresenceLocalization_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CProductInfo_SetRichPresenceLocalization_ResponseImpl(handle, isManuallyAllocated);


}
