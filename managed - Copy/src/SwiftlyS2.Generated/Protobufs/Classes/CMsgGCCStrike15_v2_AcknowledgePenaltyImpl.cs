
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_AcknowledgePenaltyImpl : TypedProtobuf<CMsgGCCStrike15_v2_AcknowledgePenalty>, CMsgGCCStrike15_v2_AcknowledgePenalty
{
  public CMsgGCCStrike15_v2_AcknowledgePenaltyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Acknowledged
  { get => Accessor.GetInt32("acknowledged"); set => Accessor.SetInt32("acknowledged", value); }

}
