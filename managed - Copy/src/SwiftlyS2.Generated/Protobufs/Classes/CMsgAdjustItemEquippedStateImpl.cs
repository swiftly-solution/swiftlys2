
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAdjustItemEquippedStateImpl : TypedProtobuf<CMsgAdjustItemEquippedState>, CMsgAdjustItemEquippedState
{
  public CMsgAdjustItemEquippedStateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }


  public uint NewClass
  { get => Accessor.GetUInt32("new_class"); set => Accessor.SetUInt32("new_class", value); }


  public uint NewSlot
  { get => Accessor.GetUInt32("new_slot"); set => Accessor.SetUInt32("new_slot", value); }


  public bool Swap
  { get => Accessor.GetBool("swap"); set => Accessor.SetBool("swap", value); }

}
