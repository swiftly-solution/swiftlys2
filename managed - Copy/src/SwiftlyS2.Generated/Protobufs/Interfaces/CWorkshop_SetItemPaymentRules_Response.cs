
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_SetItemPaymentRules_Response : ITypedProtobuf<CWorkshop_SetItemPaymentRules_Response>
{
  static CWorkshop_SetItemPaymentRules_Response ITypedProtobuf<CWorkshop_SetItemPaymentRules_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_SetItemPaymentRules_ResponseImpl(handle, isManuallyAllocated);


}
