
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCStorePurchaseFinalize : ITypedProtobuf<CMsgGCStorePurchaseFinalize>
{
  static CMsgGCStorePurchaseFinalize ITypedProtobuf<CMsgGCStorePurchaseFinalize>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCStorePurchaseFinalizeImpl(handle, isManuallyAllocated);


  public ulong TxnId { get; set; }

}
