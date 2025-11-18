
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCStorePurchaseInitResponse : ITypedProtobuf<CMsgGCStorePurchaseInitResponse>
{
  static CMsgGCStorePurchaseInitResponse ITypedProtobuf<CMsgGCStorePurchaseInitResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCStorePurchaseInitResponseImpl(handle, isManuallyAllocated);


  public int Result { get; set; }


  public ulong TxnId { get; set; }


  public string Url { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> ItemIds { get; }

}
