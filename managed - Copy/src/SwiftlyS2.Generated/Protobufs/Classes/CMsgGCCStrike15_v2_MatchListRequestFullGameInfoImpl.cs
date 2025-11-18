
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchListRequestFullGameInfoImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestFullGameInfo>, CMsgGCCStrike15_v2_MatchListRequestFullGameInfo
{
  public CMsgGCCStrike15_v2_MatchListRequestFullGameInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Matchid
  { get => Accessor.GetUInt64("matchid"); set => Accessor.SetUInt64("matchid", value); }


  public ulong Outcomeid
  { get => Accessor.GetUInt64("outcomeid"); set => Accessor.SetUInt64("outcomeid", value); }


  public uint Token
  { get => Accessor.GetUInt32("token"); set => Accessor.SetUInt32("token", value); }

}
