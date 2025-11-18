
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCGiftedItemsImpl : TypedProtobuf<CMsgGCGiftedItems>, CMsgGCGiftedItems
{
  public CMsgGCGiftedItemsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public uint Giftdefindex
  { get => Accessor.GetUInt32("giftdefindex"); set => Accessor.SetUInt32("giftdefindex", value); }


  public uint MaxGiftsPossible
  { get => Accessor.GetUInt32("max_gifts_possible"); set => Accessor.SetUInt32("max_gifts_possible", value); }


  public uint NumEligibleRecipients
  { get => Accessor.GetUInt32("num_eligible_recipients"); set => Accessor.SetUInt32("num_eligible_recipients", value); }


  public IProtobufRepeatedFieldValueType<uint> RecipientsAccountids
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "recipients_accountids"); }

}
