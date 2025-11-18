
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOAccountXpShopBidsImpl : TypedProtobuf<CSOAccountXpShopBids>, CSOAccountXpShopBids
{
  public CSOAccountXpShopBidsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint CampaignId
  { get => Accessor.GetUInt32("campaign_id"); set => Accessor.SetUInt32("campaign_id", value); }


  public uint RedeemId
  { get => Accessor.GetUInt32("redeem_id"); set => Accessor.SetUInt32("redeem_id", value); }


  public uint ExpectedCost
  { get => Accessor.GetUInt32("expected_cost"); set => Accessor.SetUInt32("expected_cost", value); }


  public uint GenerationTime
  { get => Accessor.GetUInt32("generation_time"); set => Accessor.SetUInt32("generation_time", value); }

}
