
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_GeigerImpl : NetMessage<CCSUsrMsg_Geiger>, CCSUsrMsg_Geiger
{
  public CCSUsrMsg_GeigerImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Range
  { get => Accessor.GetInt32("range"); set => Accessor.SetInt32("range", value); }

}
