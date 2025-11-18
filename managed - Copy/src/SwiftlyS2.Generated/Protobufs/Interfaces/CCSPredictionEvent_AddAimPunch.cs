
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSPredictionEvent_AddAimPunch : ITypedProtobuf<CCSPredictionEvent_AddAimPunch>
{
  static CCSPredictionEvent_AddAimPunch ITypedProtobuf<CCSPredictionEvent_AddAimPunch>.Wrap(nint handle, bool isManuallyAllocated) => new CCSPredictionEvent_AddAimPunchImpl(handle, isManuallyAllocated);


  public QAngle PunchAngle { get; set; }


  public uint WhenTick { get; set; }


  public float WhenTickFrac { get; set; }

}
