
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGCStorePurchaseInit_LineItem : ITypedProtobuf<CGCStorePurchaseInit_LineItem>
{
  static CGCStorePurchaseInit_LineItem ITypedProtobuf<CGCStorePurchaseInit_LineItem>.Wrap(nint handle, bool isManuallyAllocated) => new CGCStorePurchaseInit_LineItemImpl(handle, isManuallyAllocated);


  public uint ItemDefId { get; set; }


  public uint Quantity { get; set; }


  public uint CostInLocalCurrency { get; set; }


  public uint PurchaseType { get; set; }


  public ulong SupplementalData { get; set; }

}
