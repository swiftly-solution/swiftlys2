
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CWorkshop_SetItemPaymentRules_RequestImpl : TypedProtobuf<CWorkshop_SetItemPaymentRules_Request>, CWorkshop_SetItemPaymentRules_Request
{
  public CWorkshop_SetItemPaymentRules_RequestImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Appid
  { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }


  public uint Gameitemid
  { get => Accessor.GetUInt32("gameitemid"); set => Accessor.SetUInt32("gameitemid", value); }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRule> AssociatedWorkshopFiles
  { get => new ProtobufRepeatedFieldSubMessageType<CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRule>(Accessor, "associated_workshop_files"); }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule> PartnerAccounts
  { get => new ProtobufRepeatedFieldSubMessageType<CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule>(Accessor, "partner_accounts"); }


  public bool ValidateOnly
  { get => Accessor.GetBool("validate_only"); set => Accessor.SetBool("validate_only", value); }


  public bool MakeWorkshopFilesSubscribable
  { get => Accessor.GetBool("make_workshop_files_subscribable"); set => Accessor.SetBool("make_workshop_files_subscribable", value); }


  public CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRule AssociatedWorkshopFileForDirectPayments
  { get => new CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRuleImpl(NativeNetMessages.GetNestedMessage(Address, "associated_workshop_file_for_direct_payments"), false); }

}
