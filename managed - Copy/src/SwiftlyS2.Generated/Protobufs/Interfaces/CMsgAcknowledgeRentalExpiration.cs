
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAcknowledgeRentalExpiration : ITypedProtobuf<CMsgAcknowledgeRentalExpiration>
{
  static CMsgAcknowledgeRentalExpiration ITypedProtobuf<CMsgAcknowledgeRentalExpiration>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAcknowledgeRentalExpirationImpl(handle, isManuallyAllocated);


  public ulong CrateItemId { get; set; }

}
