
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientRequestOffers : ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestOffers>
{
  static CMsgGCCStrike15_v2_ClientRequestOffers ITypedProtobuf<CMsgGCCStrike15_v2_ClientRequestOffers>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientRequestOffersImpl(handle, isManuallyAllocated);


}
