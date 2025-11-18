
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_BarTimeImpl : NetMessage<CCSUsrMsg_BarTime>, CCSUsrMsg_BarTime
{
  public CCSUsrMsg_BarTimeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Time
  { get => Accessor.GetString("time"); set => Accessor.SetString("time", value); }

}
