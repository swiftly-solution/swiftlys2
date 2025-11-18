
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchListRequestLiveGameForUserImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchListRequestLiveGameForUser>, CMsgGCCStrike15_v2_MatchListRequestLiveGameForUser
{
  public CMsgGCCStrike15_v2_MatchListRequestLiveGameForUserImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }

}
