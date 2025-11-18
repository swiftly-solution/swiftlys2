
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_SplitScreenUserImpl : NetMessage<CNETMsg_SplitScreenUser>, CNETMsg_SplitScreenUser
{
  public CNETMsg_SplitScreenUserImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Slot
  { get => Accessor.GetInt32("slot"); set => Accessor.SetInt32("slot", value); }

}
