
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconCouponImpl : TypedProtobuf<CSOEconCoupon>, CSOEconCoupon
{
  public CSOEconCouponImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Entryid
  { get => Accessor.GetUInt32("entryid"); set => Accessor.SetUInt32("entryid", value); }


  public uint Defidx
  { get => Accessor.GetUInt32("defidx"); set => Accessor.SetUInt32("defidx", value); }


  public uint ExpirationDate
  { get => Accessor.GetUInt32("expiration_date"); set => Accessor.SetUInt32("expiration_date", value); }

}
