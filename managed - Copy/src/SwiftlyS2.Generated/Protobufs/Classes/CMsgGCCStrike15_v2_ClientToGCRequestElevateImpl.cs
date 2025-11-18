
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientToGCRequestElevateImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientToGCRequestElevate>, CMsgGCCStrike15_v2_ClientToGCRequestElevate
{
  public CMsgGCCStrike15_v2_ClientToGCRequestElevateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Stage
  { get => Accessor.GetUInt32("stage"); set => Accessor.SetUInt32("stage", value); }

}
