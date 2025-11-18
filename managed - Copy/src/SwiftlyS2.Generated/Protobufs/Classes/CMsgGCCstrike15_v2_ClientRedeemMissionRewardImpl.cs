
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCstrike15_v2_ClientRedeemMissionRewardImpl : TypedProtobuf<CMsgGCCstrike15_v2_ClientRedeemMissionReward>, CMsgGCCstrike15_v2_ClientRedeemMissionReward
{
  public CMsgGCCstrike15_v2_ClientRedeemMissionRewardImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint CampaignId
  { get => Accessor.GetUInt32("campaign_id"); set => Accessor.SetUInt32("campaign_id", value); }


  public uint RedeemId
  { get => Accessor.GetUInt32("redeem_id"); set => Accessor.SetUInt32("redeem_id", value); }


  public uint RedeemableBalance
  { get => Accessor.GetUInt32("redeemable_balance"); set => Accessor.SetUInt32("redeemable_balance", value); }


  public uint ExpectedCost
  { get => Accessor.GetUInt32("expected_cost"); set => Accessor.SetUInt32("expected_cost", value); }


  public int BidControl
  { get => Accessor.GetInt32("bid_control"); set => Accessor.SetInt32("bid_control", value); }

}
