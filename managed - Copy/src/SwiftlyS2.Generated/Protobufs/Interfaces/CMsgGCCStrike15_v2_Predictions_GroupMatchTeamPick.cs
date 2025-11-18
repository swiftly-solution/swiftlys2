
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick : ITypedProtobuf<CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick>
{
  static CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick ITypedProtobuf<CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPickImpl(handle, isManuallyAllocated);


  public int Sectionid { get; set; }


  public int Groupid { get; set; }


  public int Index { get; set; }


  public int Teamid { get; set; }


  public ulong Itemid { get; set; }

}
