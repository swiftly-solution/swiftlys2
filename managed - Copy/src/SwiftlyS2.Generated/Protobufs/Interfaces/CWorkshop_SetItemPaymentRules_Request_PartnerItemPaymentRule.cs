
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule : ITypedProtobuf<CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule>
{
  static CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule ITypedProtobuf<CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRuleImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public float RevenuePercentage { get; set; }


  public string RuleDescription { get; set; }

}
