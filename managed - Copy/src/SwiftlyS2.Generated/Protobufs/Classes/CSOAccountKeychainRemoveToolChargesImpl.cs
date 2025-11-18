
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOAccountKeychainRemoveToolChargesImpl : TypedProtobuf<CSOAccountKeychainRemoveToolCharges>, CSOAccountKeychainRemoveToolCharges
{
  public CSOAccountKeychainRemoveToolChargesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Charges
  { get => Accessor.GetUInt32("charges"); set => Accessor.SetUInt32("charges", value); }

}
