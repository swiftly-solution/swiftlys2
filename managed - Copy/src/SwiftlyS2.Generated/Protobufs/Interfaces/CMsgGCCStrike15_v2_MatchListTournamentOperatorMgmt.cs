
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmt : ITypedProtobuf<CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmt>
{
  static CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmt ITypedProtobuf<CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmt>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmtImpl(handle, isManuallyAllocated);


  public int Eventid { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_MatchInfo> Matches { get; }


  public uint Accountid { get; set; }

}
