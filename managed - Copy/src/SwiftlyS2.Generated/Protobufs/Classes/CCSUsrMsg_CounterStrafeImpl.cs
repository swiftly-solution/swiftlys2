
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_CounterStrafeImpl : NetMessage<CCSUsrMsg_CounterStrafe>, CCSUsrMsg_CounterStrafe
{
  public CCSUsrMsg_CounterStrafeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int PressToReleaseNs
  { get => Accessor.GetInt32("press_to_release_ns"); set => Accessor.SetInt32("press_to_release_ns", value); }


  public int TotalKeysDown
  { get => Accessor.GetInt32("total_keys_down"); set => Accessor.SetInt32("total_keys_down", value); }

}
