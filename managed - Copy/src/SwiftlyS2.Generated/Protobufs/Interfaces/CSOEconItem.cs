
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconItem : ITypedProtobuf<CSOEconItem>
{
  static CSOEconItem ITypedProtobuf<CSOEconItem>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconItemImpl(handle, isManuallyAllocated);


  public ulong Id { get; set; }


  public uint AccountId { get; set; }


  public uint Inventory { get; set; }


  public uint DefIndex { get; set; }


  public uint Quantity { get; set; }


  public uint Level { get; set; }


  public uint Quality { get; set; }


  public uint Flags { get; set; }


  public uint Origin { get; set; }


  public string CustomName { get; set; }


  public string CustomDesc { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSOEconItemAttribute> Attribute { get; }


  public CSOEconItem InteriorItem { get; }


  public bool InUse { get; set; }


  public uint Style { get; set; }


  public ulong OriginalId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSOEconItemEquipped> EquippedState { get; }


  public uint Rarity { get; set; }

}
