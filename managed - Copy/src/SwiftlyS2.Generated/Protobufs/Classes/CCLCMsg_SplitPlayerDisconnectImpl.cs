
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_SplitPlayerDisconnectImpl : NetMessage<CCLCMsg_SplitPlayerDisconnect>, CCLCMsg_SplitPlayerDisconnect
{
  public CCLCMsg_SplitPlayerDisconnectImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Slot
  { get => Accessor.GetInt32("slot"); set => Accessor.SetInt32("slot", value); }

}
