
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCstrike15_v2_GC2ServerNotifyXPRewardedImpl : TypedProtobuf<CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded>, CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded
{
  public CMsgGCCstrike15_v2_GC2ServerNotifyXPRewardedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<XpProgressData> XpProgressData
  { get => new ProtobufRepeatedFieldSubMessageType<XpProgressData>(Accessor, "xp_progress_data"); }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint CurrentXp
  { get => Accessor.GetUInt32("current_xp"); set => Accessor.SetUInt32("current_xp", value); }


  public uint CurrentLevel
  { get => Accessor.GetUInt32("current_level"); set => Accessor.SetUInt32("current_level", value); }


  public uint UpgradedDefidx
  { get => Accessor.GetUInt32("upgraded_defidx"); set => Accessor.SetUInt32("upgraded_defidx", value); }


  public uint OperationPointsAwarded
  { get => Accessor.GetUInt32("operation_points_awarded"); set => Accessor.SetUInt32("operation_points_awarded", value); }


  public uint FreeRewards
  { get => Accessor.GetUInt32("free_rewards"); set => Accessor.SetUInt32("free_rewards", value); }


  public uint XpTrailRemaining
  { get => Accessor.GetUInt32("xp_trail_remaining"); set => Accessor.SetUInt32("xp_trail_remaining", value); }


  public int XpTrailXpNeeded
  { get => Accessor.GetInt32("xp_trail_xp_needed"); set => Accessor.SetInt32("xp_trail_xp_needed", value); }


  public uint XpTrailLevel
  { get => Accessor.GetUInt32("xp_trail_level"); set => Accessor.SetUInt32("xp_trail_level", value); }

}
