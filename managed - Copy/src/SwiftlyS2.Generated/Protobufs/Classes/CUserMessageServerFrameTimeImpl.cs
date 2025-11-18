
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageServerFrameTimeImpl : NetMessage<CUserMessageServerFrameTime>, CUserMessageServerFrameTime
{
  public CUserMessageServerFrameTimeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float FrameTime
  { get => Accessor.GetFloat("frame_time"); set => Accessor.SetFloat("frame_time", value); }

}
