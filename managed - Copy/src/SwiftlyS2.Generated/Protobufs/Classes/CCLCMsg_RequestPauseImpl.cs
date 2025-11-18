
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_RequestPauseImpl : NetMessage<CCLCMsg_RequestPause>, CCLCMsg_RequestPause
{
  public CCLCMsg_RequestPauseImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public RequestPause_t PauseType
  { get => (RequestPause_t)Accessor.GetInt32("pause_type"); set => Accessor.SetInt32("pause_type", (int)value); }


  public int PauseGroup
  { get => Accessor.GetInt32("pause_group"); set => Accessor.SetInt32("pause_group", value); }

}
