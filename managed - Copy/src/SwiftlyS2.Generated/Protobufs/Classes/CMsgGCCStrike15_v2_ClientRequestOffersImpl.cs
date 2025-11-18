
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientRequestOffersImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientRequestOffers>, CMsgGCCStrike15_v2_ClientRequestOffers
{
  public CMsgGCCStrike15_v2_ClientRequestOffersImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


}
