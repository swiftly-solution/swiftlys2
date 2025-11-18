
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageItemPickupImpl : NetMessage<CUserMessageItemPickup>, CUserMessageItemPickup
{
  public CUserMessageItemPickupImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Itemname
  { get => Accessor.GetString("itemname"); set => Accessor.SetString("itemname", value); }

}
