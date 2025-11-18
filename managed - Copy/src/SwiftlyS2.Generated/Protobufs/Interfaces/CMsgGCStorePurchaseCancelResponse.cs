
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCStorePurchaseCancelResponse : ITypedProtobuf<CMsgGCStorePurchaseCancelResponse>
{
  static CMsgGCStorePurchaseCancelResponse ITypedProtobuf<CMsgGCStorePurchaseCancelResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCStorePurchaseCancelResponseImpl(handle, isManuallyAllocated);


  public uint Result { get; set; }

}
