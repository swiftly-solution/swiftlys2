
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRuleImpl : TypedProtobuf<CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRule>, CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRule
{
  public CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRuleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong WorkshopFileId
  { get => Accessor.GetUInt64("workshop_file_id"); set => Accessor.SetUInt64("workshop_file_id", value); }


  public float RevenuePercentage
  { get => Accessor.GetFloat("revenue_percentage"); set => Accessor.SetFloat("revenue_percentage", value); }


  public string RuleDescription
  { get => Accessor.GetString("rule_description"); set => Accessor.SetString("rule_description", value); }


  public uint RuleType
  { get => Accessor.GetUInt32("rule_type"); set => Accessor.SetUInt32("rule_type", value); }

}
