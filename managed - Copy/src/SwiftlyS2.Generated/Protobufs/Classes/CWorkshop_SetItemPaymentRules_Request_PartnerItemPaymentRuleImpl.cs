
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRuleImpl : TypedProtobuf<CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule>, CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule
{
  public CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRuleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public float RevenuePercentage
  { get => Accessor.GetFloat("revenue_percentage"); set => Accessor.SetFloat("revenue_percentage", value); }


  public string RuleDescription
  { get => Accessor.GetString("rule_description"); set => Accessor.SetString("rule_description", value); }

}
