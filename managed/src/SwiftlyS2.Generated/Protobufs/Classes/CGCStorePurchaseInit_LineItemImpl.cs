
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGCStorePurchaseInit_LineItemImpl : TypedProtobuf<CGCStorePurchaseInit_LineItem>, CGCStorePurchaseInit_LineItem
{
  public CGCStorePurchaseInit_LineItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ItemDefId
  { get => Accessor.GetUInt32("item_def_id"); set => Accessor.SetUInt32("item_def_id", value); }


  public uint Quantity
  { get => Accessor.GetUInt32("quantity"); set => Accessor.SetUInt32("quantity", value); }


  public uint CostInLocalCurrency
  { get => Accessor.GetUInt32("cost_in_local_currency"); set => Accessor.SetUInt32("cost_in_local_currency", value); }


  public uint PurchaseType
  { get => Accessor.GetUInt32("purchase_type"); set => Accessor.SetUInt32("purchase_type", value); }


  public ulong SupplementalData
  { get => Accessor.GetUInt64("supplemental_data"); set => Accessor.SetUInt64("supplemental_data", value); }

}
