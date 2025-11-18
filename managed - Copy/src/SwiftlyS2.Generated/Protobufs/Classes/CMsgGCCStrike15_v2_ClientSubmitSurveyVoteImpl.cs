
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientSubmitSurveyVoteImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientSubmitSurveyVote>, CMsgGCCStrike15_v2_ClientSubmitSurveyVote
{
  public CMsgGCCStrike15_v2_ClientSubmitSurveyVoteImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint SurveyId
  { get => Accessor.GetUInt32("survey_id"); set => Accessor.SetUInt32("survey_id", value); }


  public uint Vote
  { get => Accessor.GetUInt32("vote"); set => Accessor.SetUInt32("vote", value); }

}
