
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_AcknowledgePenalty : ITypedProtobuf<CMsgGCCStrike15_v2_AcknowledgePenalty>
{
  static CMsgGCCStrike15_v2_AcknowledgePenalty ITypedProtobuf<CMsgGCCStrike15_v2_AcknowledgePenalty>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_AcknowledgePenaltyImpl(handle, isManuallyAllocated);


  public int Acknowledged { get; set; }

}
