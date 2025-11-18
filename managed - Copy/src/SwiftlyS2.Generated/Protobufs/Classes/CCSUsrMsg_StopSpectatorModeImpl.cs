
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_StopSpectatorModeImpl : NetMessage<CCSUsrMsg_StopSpectatorMode>, CCSUsrMsg_StopSpectatorMode
{
  public CCSUsrMsg_StopSpectatorModeImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Dummy
  { get => Accessor.GetInt32("dummy"); set => Accessor.SetInt32("dummy", value); }

}
