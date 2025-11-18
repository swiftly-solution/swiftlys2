
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_AddSpecialPayment_Request : ITypedProtobuf<CWorkshop_AddSpecialPayment_Request>
{
  static CWorkshop_AddSpecialPayment_Request ITypedProtobuf<CWorkshop_AddSpecialPayment_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_AddSpecialPayment_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public uint Gameitemid { get; set; }


  public string Date { get; set; }


  public ulong PaymentUsUsd { get; set; }


  public ulong PaymentRowUsd { get; set; }

}
