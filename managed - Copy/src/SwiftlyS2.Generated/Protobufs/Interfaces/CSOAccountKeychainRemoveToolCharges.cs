
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountKeychainRemoveToolCharges : ITypedProtobuf<CSOAccountKeychainRemoveToolCharges>
{
  static CSOAccountKeychainRemoveToolCharges ITypedProtobuf<CSOAccountKeychainRemoveToolCharges>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountKeychainRemoveToolChargesImpl(handle, isManuallyAllocated);


  public uint Charges { get; set; }

}
