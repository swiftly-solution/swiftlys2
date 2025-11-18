
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_FadeImpl : NetMessage<CCSUsrMsg_Fade>, CCSUsrMsg_Fade
{
  public CCSUsrMsg_FadeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Duration
  { get => Accessor.GetInt32("duration"); set => Accessor.SetInt32("duration", value); }


  public int HoldTime
  { get => Accessor.GetInt32("hold_time"); set => Accessor.SetInt32("hold_time", value); }


  public int Flags
  { get => Accessor.GetInt32("flags"); set => Accessor.SetInt32("flags", value); }


  public Color Clr
  { get => Accessor.GetColor("clr"); set => Accessor.SetColor("clr", value); }

}
