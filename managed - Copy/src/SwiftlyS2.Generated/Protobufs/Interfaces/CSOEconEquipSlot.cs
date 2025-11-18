
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconEquipSlot : ITypedProtobuf<CSOEconEquipSlot>
{
  static CSOEconEquipSlot ITypedProtobuf<CSOEconEquipSlot>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconEquipSlotImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint ClassId { get; set; }


  public uint SlotId { get; set; }


  public ulong ItemId { get; set; }


  public uint ItemDefinition { get; set; }

}
