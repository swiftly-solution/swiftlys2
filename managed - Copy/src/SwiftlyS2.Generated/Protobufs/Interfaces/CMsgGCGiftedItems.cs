
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCGiftedItems : ITypedProtobuf<CMsgGCGiftedItems>
{
  static CMsgGCGiftedItems ITypedProtobuf<CMsgGCGiftedItems>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCGiftedItemsImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public uint Giftdefindex { get; set; }


  public uint MaxGiftsPossible { get; set; }


  public uint NumEligibleRecipients { get; set; }


  public IProtobufRepeatedFieldValueType<uint> RecipientsAccountids { get; }

}
