
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconItemEquippedImpl : TypedProtobuf<CSOEconItemEquipped>, CSOEconItemEquipped
{
  public CSOEconItemEquippedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint NewClass
  { get => Accessor.GetUInt32("new_class"); set => Accessor.SetUInt32("new_class", value); }


  public uint NewSlot
  { get => Accessor.GetUInt32("new_slot"); set => Accessor.SetUInt32("new_slot", value); }

}
