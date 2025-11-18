
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCUserTrackTimePlayedConsecutivelyImpl : TypedProtobuf<CMsgGCUserTrackTimePlayedConsecutively>, CMsgGCUserTrackTimePlayedConsecutively
{
  public CMsgGCUserTrackTimePlayedConsecutivelyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint State
  { get => Accessor.GetUInt32("state"); set => Accessor.SetUInt32("state", value); }

}
