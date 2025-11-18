
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CWorkshop_SetItemPaymentRules_Request : ITypedProtobuf<CWorkshop_SetItemPaymentRules_Request>
{
  static CWorkshop_SetItemPaymentRules_Request ITypedProtobuf<CWorkshop_SetItemPaymentRules_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CWorkshop_SetItemPaymentRules_RequestImpl(handle, isManuallyAllocated);


  public uint Appid { get; set; }


  public uint Gameitemid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_SetItemPaymentRules_Request_WorkshopItemPaymentRule> AssociatedWorkshopFiles { get; }


  public IProtobufRepeatedFieldSubMessageType<CWorkshop_SetItemPaymentRules_Request_PartnerItemPaymentRule> PartnerAccounts { get; }


  public bool ValidateOnly { get; set; }


  public bool MakeWorkshopFilesSubscribable { get; set; }


  public CWorkshop_SetItemPaymentRules_Request_WorkshopDirectPaymentRule AssociatedWorkshopFileForDirectPayments { get; }

}
