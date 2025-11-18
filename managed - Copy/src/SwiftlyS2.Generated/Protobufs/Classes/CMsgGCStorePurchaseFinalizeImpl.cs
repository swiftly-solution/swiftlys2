
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCStorePurchaseFinalizeImpl : TypedProtobuf<CMsgGCStorePurchaseFinalize>, CMsgGCStorePurchaseFinalize
{
  public CMsgGCStorePurchaseFinalizeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong TxnId
  { get => Accessor.GetUInt64("txn_id"); set => Accessor.SetUInt64("txn_id", value); }

}
