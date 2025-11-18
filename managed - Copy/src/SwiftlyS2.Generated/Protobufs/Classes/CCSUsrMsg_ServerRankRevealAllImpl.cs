
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ServerRankRevealAllImpl : NetMessage<CCSUsrMsg_ServerRankRevealAll>, CCSUsrMsg_ServerRankRevealAll
{
  public CCSUsrMsg_ServerRankRevealAllImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int SecondsTillShutdown
  { get => Accessor.GetInt32("seconds_till_shutdown"); set => Accessor.SetInt32("seconds_till_shutdown", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl(NativeNetMessages.GetNestedMessage(Address, "reservation"), false); }

}
