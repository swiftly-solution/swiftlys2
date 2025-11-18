
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandon : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandon>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandon ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandon>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandonImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve AbandonedMatch { get; }


  public uint PenaltySeconds { get; set; }


  public uint PenaltyReason { get; set; }

}
