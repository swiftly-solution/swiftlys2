
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgIPCAddressImpl : TypedProtobuf<CMsgIPCAddress>, CMsgIPCAddress
{
  public CMsgIPCAddressImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ComputerGuid
  { get => Accessor.GetUInt64("computer_guid"); set => Accessor.SetUInt64("computer_guid", value); }


  public uint ProcessId
  { get => Accessor.GetUInt32("process_id"); set => Accessor.SetUInt32("process_id", value); }

}
