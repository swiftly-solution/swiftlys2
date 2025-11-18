
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandonImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandon>, CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandon
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ClientAbandonImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ClientReserve AbandonedMatch
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ClientReserveImpl(NativeNetMessages.GetNestedMessage(Address, "abandoned_match"), false); }


  public uint PenaltySeconds
  { get => Accessor.GetUInt32("penalty_seconds"); set => Accessor.SetUInt32("penalty_seconds", value); }


  public uint PenaltyReason
  { get => Accessor.GetUInt32("penalty_reason"); set => Accessor.SetUInt32("penalty_reason", value); }

}
