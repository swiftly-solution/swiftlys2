
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientSubmitSurveyVote : ITypedProtobuf<CMsgGCCStrike15_v2_ClientSubmitSurveyVote>
{
  static CMsgGCCStrike15_v2_ClientSubmitSurveyVote ITypedProtobuf<CMsgGCCStrike15_v2_ClientSubmitSurveyVote>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientSubmitSurveyVoteImpl(handle, isManuallyAllocated);


  public uint SurveyId { get; set; }


  public uint Vote { get; set; }

}
