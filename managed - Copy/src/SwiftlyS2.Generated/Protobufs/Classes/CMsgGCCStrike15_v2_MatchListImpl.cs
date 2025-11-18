
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchListImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchList>, CMsgGCCStrike15_v2_MatchList
{
  public CMsgGCCStrike15_v2_MatchListImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Msgrequestid
  { get => Accessor.GetUInt32("msgrequestid"); set => Accessor.SetUInt32("msgrequestid", value); }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public uint Servertime
  { get => Accessor.GetUInt32("servertime"); set => Accessor.SetUInt32("servertime", value); }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_MatchInfo> Matches
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_MatchInfo>(Accessor, "matches"); }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> Streams
  { get => new ProtobufRepeatedFieldSubMessageType<TournamentTeam>(Accessor, "streams"); }


  public CDataGCCStrike15_v2_TournamentInfo Tournamentinfo
  { get => new CDataGCCStrike15_v2_TournamentInfoImpl(NativeNetMessages.GetNestedMessage(Address, "tournamentinfo"), false); }

}
