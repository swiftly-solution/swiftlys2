
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ItemPickupImpl : NetMessage<CCSUsrMsg_ItemPickup>, CCSUsrMsg_ItemPickup
{
  public CCSUsrMsg_ItemPickupImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Item
  { get => Accessor.GetString("item"); set => Accessor.SetString("item", value); }

}
