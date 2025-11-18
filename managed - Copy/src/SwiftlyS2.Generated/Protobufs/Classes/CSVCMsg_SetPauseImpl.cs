
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_SetPauseImpl : NetMessage<CSVCMsg_SetPause>, CSVCMsg_SetPause
{
  public CSVCMsg_SetPauseImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool Paused
  { get => Accessor.GetBool("paused"); set => Accessor.SetBool("paused", value); }

}
