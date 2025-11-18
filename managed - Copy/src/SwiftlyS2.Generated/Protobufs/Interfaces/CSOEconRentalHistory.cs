
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconRentalHistory : ITypedProtobuf<CSOEconRentalHistory>
{
  static CSOEconRentalHistory ITypedProtobuf<CSOEconRentalHistory>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconRentalHistoryImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public ulong CrateItemId { get; set; }


  public uint CrateDefIndex { get; set; }


  public uint IssueDate { get; set; }


  public uint ExpirationDate { get; set; }

}
