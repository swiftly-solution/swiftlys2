
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SSUIImpl : NetMessage<CCSUsrMsg_SSUI>, CCSUsrMsg_SSUI
{
  public CCSUsrMsg_SSUIImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public bool Show
  { get => Accessor.GetBool("show"); set => Accessor.SetBool("show", value); }


  public float StartTime
  { get => Accessor.GetFloat("start_time"); set => Accessor.SetFloat("start_time", value); }


  public float EndTime
  { get => Accessor.GetFloat("end_time"); set => Accessor.SetFloat("end_time", value); }

}
