
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Predictions : ITypedProtobuf<CMsgGCCStrike15_v2_Predictions>
{
  static CMsgGCCStrike15_v2_Predictions ITypedProtobuf<CMsgGCCStrike15_v2_Predictions>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PredictionsImpl(handle, isManuallyAllocated);


  public uint EventId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick> GroupMatchTeamPicks { get; }

}
