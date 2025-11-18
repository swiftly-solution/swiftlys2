
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_AddSpecialPayment_RequestImpl : TypedProtobuf<CWorkshop_AddSpecialPayment_Request>, CWorkshop_AddSpecialPayment_Request
{
  public CWorkshop_AddSpecialPayment_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public uint Gameitemid
  { get => Accessor.GetUInt32("gameitemid"); set => Accessor.SetUInt32("gameitemid", value); }


  public string Date
  { get => Accessor.GetString("date"); set => Accessor.SetString("date", value); }


  public ulong PaymentUsUsd
  { get => Accessor.GetUInt64("payment_us_usd"); set => Accessor.SetUInt64("payment_us_usd", value); }


  public ulong PaymentRowUsd
  { get => Accessor.GetUInt64("payment_row_usd"); set => Accessor.SetUInt64("payment_row_usd", value); }

}
