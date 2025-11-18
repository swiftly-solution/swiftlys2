
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCStorePurchaseInit : ITypedProtobuf<CMsgGCStorePurchaseInit>
{
  static CMsgGCStorePurchaseInit ITypedProtobuf<CMsgGCStorePurchaseInit>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCStorePurchaseInitImpl(handle, isManuallyAllocated);


  public string Country { get; set; }


  public int Language { get; set; }


  public int Currency { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CGCStorePurchaseInit_LineItem> LineItems { get; }

}
