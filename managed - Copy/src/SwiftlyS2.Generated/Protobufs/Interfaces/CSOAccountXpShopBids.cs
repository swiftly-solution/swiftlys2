
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountXpShopBids : ITypedProtobuf<CSOAccountXpShopBids>
{
  static CSOAccountXpShopBids ITypedProtobuf<CSOAccountXpShopBids>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountXpShopBidsImpl(handle, isManuallyAllocated);


  public uint CampaignId { get; set; }


  public uint RedeemId { get; set; }


  public uint ExpectedCost { get; set; }


  public uint GenerationTime { get; set; }

}
