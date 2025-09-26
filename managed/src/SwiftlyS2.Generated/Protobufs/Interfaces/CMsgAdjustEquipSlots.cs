
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgAdjustEquipSlots : ITypedProtobuf<CMsgAdjustEquipSlots>
{
  static CMsgAdjustEquipSlots ITypedProtobuf<CMsgAdjustEquipSlots>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgAdjustEquipSlotsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgAdjustEquipSlot> Slots { get; }


  public uint ChangeNum { get; set; }

}
