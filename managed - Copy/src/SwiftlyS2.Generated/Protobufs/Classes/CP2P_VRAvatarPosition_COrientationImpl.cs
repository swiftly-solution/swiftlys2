
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CP2P_VRAvatarPosition_COrientationImpl : TypedProtobuf<CP2P_VRAvatarPosition_COrientation>, CP2P_VRAvatarPosition_COrientation
{
  public CP2P_VRAvatarPosition_COrientationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public Vector Pos
  { get => Accessor.GetVector("pos"); set => Accessor.SetVector("pos", value); }


  public QAngle Ang
  { get => Accessor.GetQAngle("ang"); set => Accessor.SetQAngle("ang", value); }

}
