
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOAccountSeasonalOperationImpl : TypedProtobuf<CSOAccountSeasonalOperation>, CSOAccountSeasonalOperation
{
  public CSOAccountSeasonalOperationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint SeasonValue
  { get => Accessor.GetUInt32("season_value"); set => Accessor.SetUInt32("season_value", value); }


  public uint TierUnlocked
  { get => Accessor.GetUInt32("tier_unlocked"); set => Accessor.SetUInt32("tier_unlocked", value); }


  public uint PremiumTiers
  { get => Accessor.GetUInt32("premium_tiers"); set => Accessor.SetUInt32("premium_tiers", value); }


  public uint MissionId
  { get => Accessor.GetUInt32("mission_id"); set => Accessor.SetUInt32("mission_id", value); }


  public uint MissionsCompleted
  { get => Accessor.GetUInt32("missions_completed"); set => Accessor.SetUInt32("missions_completed", value); }


  public uint RedeemableBalance
  { get => Accessor.GetUInt32("redeemable_balance"); set => Accessor.SetUInt32("redeemable_balance", value); }


  public uint SeasonPassTime
  { get => Accessor.GetUInt32("season_pass_time"); set => Accessor.SetUInt32("season_pass_time", value); }

}
