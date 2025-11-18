
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconClaimCode : ITypedProtobuf<CSOEconClaimCode>
{
  static CSOEconClaimCode ITypedProtobuf<CSOEconClaimCode>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconClaimCodeImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint CodeType { get; set; }


  public uint TimeAcquired { get; set; }


  public string Code { get; set; }

}
