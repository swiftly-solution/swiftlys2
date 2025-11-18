
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_ServerStatusImpl : NetMessage<CCLCMsg_ServerStatus>, CCLCMsg_ServerStatus
{
  public CCLCMsg_ServerStatusImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool Simplified
  { get => Accessor.GetBool("simplified"); set => Accessor.SetBool("simplified", value); }

}
