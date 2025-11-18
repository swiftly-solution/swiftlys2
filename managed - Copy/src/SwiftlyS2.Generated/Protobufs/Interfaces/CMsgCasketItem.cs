
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgCasketItem : ITypedProtobuf<CMsgCasketItem>
{
  static CMsgCasketItem ITypedProtobuf<CMsgCasketItem>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgCasketItemImpl(handle, isManuallyAllocated);


  public ulong CasketItemId { get; set; }


  public ulong ItemItemId { get; set; }

}
