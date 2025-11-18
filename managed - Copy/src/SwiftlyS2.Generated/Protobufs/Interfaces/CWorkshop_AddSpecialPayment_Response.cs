
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_AddSpecialPayment_Response : ITypedProtobuf<CWorkshop_AddSpecialPayment_Response>
{
  static CWorkshop_AddSpecialPayment_Response ITypedProtobuf<CWorkshop_AddSpecialPayment_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_AddSpecialPayment_ResponseImpl(handle, isManuallyAllocated);


}
