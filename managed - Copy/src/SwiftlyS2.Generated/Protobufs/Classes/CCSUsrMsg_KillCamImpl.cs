
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_KillCamImpl : NetMessage<CCSUsrMsg_KillCam>, CCSUsrMsg_KillCam
{
  public CCSUsrMsg_KillCamImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int ObsMode
  { get => Accessor.GetInt32("obs_mode"); set => Accessor.SetInt32("obs_mode", value); }


  public int FirstTarget
  { get => Accessor.GetInt32("first_target"); set => Accessor.SetInt32("first_target", value); }


  public int SecondTarget
  { get => Accessor.GetInt32("second_target"); set => Accessor.SetInt32("second_target", value); }

}
