
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ResetHudImpl : NetMessage<CCSUsrMsg_ResetHud>, CCSUsrMsg_ResetHud
{
  public CCSUsrMsg_ResetHudImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool Reset
  { get => Accessor.GetBool("reset"); set => Accessor.SetBool("reset", value); }

}
