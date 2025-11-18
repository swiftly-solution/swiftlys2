
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ServerNotificationForUserPenalty : ITypedProtobuf<CMsgGCCStrike15_v2_ServerNotificationForUserPenalty>
{
  static CMsgGCCStrike15_v2_ServerNotificationForUserPenalty ITypedProtobuf<CMsgGCCStrike15_v2_ServerNotificationForUserPenalty>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ServerNotificationForUserPenaltyImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint Reason { get; set; }


  public uint Seconds { get; set; }


  public bool CommunicationCooldown { get; set; }

}
