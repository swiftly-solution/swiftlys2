
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgDevNewItemRequest : ITypedProtobuf<CMsgDevNewItemRequest>
{
  static CMsgDevNewItemRequest ITypedProtobuf<CMsgDevNewItemRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgDevNewItemRequestImpl(handle, isManuallyAllocated);


  public ulong Receiver { get; set; }


  public CSOItemCriteria Criteria { get; }

}
