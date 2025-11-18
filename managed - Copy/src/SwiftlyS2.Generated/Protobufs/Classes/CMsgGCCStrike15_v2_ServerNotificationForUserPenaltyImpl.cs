
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ServerNotificationForUserPenaltyImpl : TypedProtobuf<CMsgGCCStrike15_v2_ServerNotificationForUserPenalty>, CMsgGCCStrike15_v2_ServerNotificationForUserPenalty
{
  public CMsgGCCStrike15_v2_ServerNotificationForUserPenaltyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint Reason
  { get => Accessor.GetUInt32("reason"); set => Accessor.SetUInt32("reason", value); }


  public uint Seconds
  { get => Accessor.GetUInt32("seconds"); set => Accessor.SetUInt32("seconds", value); }


  public bool CommunicationCooldown
  { get => Accessor.GetBool("communication_cooldown"); set => Accessor.SetBool("communication_cooldown", value); }

}
