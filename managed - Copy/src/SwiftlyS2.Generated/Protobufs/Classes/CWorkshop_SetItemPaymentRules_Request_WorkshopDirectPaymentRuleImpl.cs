
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRuleImpl : TypedProtobuf<CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRule>, CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRule
{
  public CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRuleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong WorkshopFileId
  { get => Accessor.GetUInt64("workshop_file_id"); set => Accessor.SetUInt64("workshop_file_id", value); }


  public string RuleDescription
  { get => Accessor.GetString("rule_description"); set => Accessor.SetString("rule_description", value); }

}
