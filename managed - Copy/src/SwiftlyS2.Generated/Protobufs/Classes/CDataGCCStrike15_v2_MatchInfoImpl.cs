
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_MatchInfoImpl : TypedProtobuf<CDataGCCStrike15_v2_MatchInfo>, CDataGCCStrike15_v2_MatchInfo
{
  public CDataGCCStrike15_v2_MatchInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Matchid
  { get => Accessor.GetUInt64("matchid"); set => Accessor.SetUInt64("matchid", value); }


  public uint Matchtime
  { get => Accessor.GetUInt32("matchtime"); set => Accessor.SetUInt32("matchtime", value); }


  public WatchableMatchInfo Watchablematchinfo
  { get => new WatchableMatchInfoImpl(NativeNetMessages.GetNestedMessage(Address, "watchablematchinfo"), false); }


  public CMsgGCCStrike15_v2_MatchmakingServerRoundStats RoundstatsLegacy
  { get => new CMsgGCCStrike15_v2_MatchmakingServerRoundStatsImpl(NativeNetMessages.GetNestedMessage(Address, "roundstats_legacy"), false); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingServerRoundStats> Roundstatsall
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingServerRoundStats>(Accessor, "roundstatsall"); }

}
