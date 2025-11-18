
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageFadeImpl : NetMessage<CUserMessageFade>, CUserMessageFade
{
  public CUserMessageFadeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Duration
  { get => Accessor.GetUInt32("duration"); set => Accessor.SetUInt32("duration", value); }


  public uint HoldTime
  { get => Accessor.GetUInt32("hold_time"); set => Accessor.SetUInt32("hold_time", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }

}
