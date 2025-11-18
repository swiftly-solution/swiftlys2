
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCStorePurchaseCancel : ITypedProtobuf<CMsgGCStorePurchaseCancel>
{
  static CMsgGCStorePurchaseCancel ITypedProtobuf<CMsgGCStorePurchaseCancel>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCStorePurchaseCancelImpl(handle, isManuallyAllocated);


  public ulong TxnId { get; set; }

}
