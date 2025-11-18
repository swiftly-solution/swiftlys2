
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPredictionEvent_Teleport : ITypedProtobuf<CPredictionEvent_Teleport>
{
  static CPredictionEvent_Teleport ITypedProtobuf<CPredictionEvent_Teleport>.Wrap(nint handle, bool isManuallyAllocated) => new CPredictionEvent_TeleportImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public QAngle Angles { get; set; }


  public float DropToGroundRange { get; set; }

}
