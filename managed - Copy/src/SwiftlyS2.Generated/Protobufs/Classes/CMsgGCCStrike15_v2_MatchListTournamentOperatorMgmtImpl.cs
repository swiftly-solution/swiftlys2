
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmtImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmt>, CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmt
{
  public CMsgGCCStrike15_v2_MatchListTournamentOperatorMgmtImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eventid
  { get => Accessor.GetInt32("eventid"); set => Accessor.SetInt32("eventid", value); }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_MatchInfo> Matches
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_MatchInfo>(Accessor, "matches"); }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }

}
