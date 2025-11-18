
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_AddSpecialPayment_ResponseImpl : TypedProtobuf<CWorkshop_AddSpecialPayment_Response>, CWorkshop_AddSpecialPayment_Response
{
  public CWorkshop_AddSpecialPayment_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


}
