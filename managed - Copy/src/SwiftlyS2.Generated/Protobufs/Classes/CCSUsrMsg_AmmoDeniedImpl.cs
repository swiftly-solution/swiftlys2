
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_AmmoDeniedImpl : NetMessage<CCSUsrMsg_AmmoDenied>, CCSUsrMsg_AmmoDenied
{
  public CCSUsrMsg_AmmoDeniedImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Ammoidx
  { get => Accessor.GetInt32("ammoidx"); set => Accessor.SetInt32("ammoidx", value); }

}
