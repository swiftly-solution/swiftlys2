
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_CurrentTimescaleImpl : NetMessage<CCSUsrMsg_CurrentTimescale>, CCSUsrMsg_CurrentTimescale
{
  public CCSUsrMsg_CurrentTimescaleImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public float CurTimescale
  { get => Accessor.GetFloat("cur_timescale"); set => Accessor.SetFloat("cur_timescale", value); }

}
