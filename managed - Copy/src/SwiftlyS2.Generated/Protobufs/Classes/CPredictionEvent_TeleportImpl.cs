
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPredictionEvent_TeleportImpl : TypedProtobuf<CPredictionEvent_Teleport>, CPredictionEvent_Teleport
{
  public CPredictionEvent_TeleportImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public float DropToGroundRange
  { get => Accessor.GetFloat("drop_to_ground_range"); set => Accessor.SetFloat("drop_to_ground_range", value); }

}
