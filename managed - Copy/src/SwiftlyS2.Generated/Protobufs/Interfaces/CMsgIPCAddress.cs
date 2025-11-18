
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgIPCAddress : ITypedProtobuf<CMsgIPCAddress>
{
  static CMsgIPCAddress ITypedProtobuf<CMsgIPCAddress>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgIPCAddressImpl(handle, isManuallyAllocated);


  public ulong ComputerGuid { get; set; }


  public uint ProcessId { get; set; }

}
