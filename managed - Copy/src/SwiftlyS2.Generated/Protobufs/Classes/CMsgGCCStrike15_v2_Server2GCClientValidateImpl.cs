
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Server2GCClientValidateImpl : TypedProtobuf<CMsgGCCStrike15_v2_Server2GCClientValidate>, CMsgGCCStrike15_v2_Server2GCClientValidate
{
  public CMsgGCCStrike15_v2_Server2GCClientValidateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }

}
