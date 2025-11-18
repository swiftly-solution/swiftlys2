
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_HltvFixupOperatorTickImpl : TypedProtobuf<CCLCMsg_HltvFixupOperatorTick>, CCLCMsg_HltvFixupOperatorTick
{
  public CCLCMsg_HltvFixupOperatorTickImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Tick
  { get => Accessor.GetInt32("tick"); set => Accessor.SetInt32("tick", value); }


  public byte[] PropsData
  { get => Accessor.GetBytes("props_data"); set => Accessor.SetBytes("props_data", value); }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public QAngle EyeAngles
  { get => Accessor.GetQAngle("eye_angles"); set => Accessor.SetQAngle("eye_angles", value); }


  public int ObserverMode
  { get => Accessor.GetInt32("observer_mode"); set => Accessor.SetInt32("observer_mode", value); }


  public bool CameramanScoreboard
  { get => Accessor.GetBool("cameraman_scoreboard"); set => Accessor.SetBool("cameraman_scoreboard", value); }


  public int ObserverTarget
  { get => Accessor.GetInt32("observer_target"); set => Accessor.SetInt32("observer_target", value); }


  public Vector ViewOffset
  { get => Accessor.GetVector("view_offset"); set => Accessor.SetVector("view_offset", value); }

}
