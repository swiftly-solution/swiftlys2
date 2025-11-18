
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PredictionsImpl : TypedProtobuf<CMsgGCCStrike15_v2_Predictions>, CMsgGCCStrike15_v2_Predictions
{
  public CMsgGCCStrike15_v2_PredictionsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint EventId
  { get => Accessor.GetUInt32("event_id"); set => Accessor.SetUInt32("event_id", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick> GroupMatchTeamPicks
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick>(Accessor, "group_match_team_picks"); }

}
