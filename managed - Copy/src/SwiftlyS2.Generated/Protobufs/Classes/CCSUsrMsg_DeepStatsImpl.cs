
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_DeepStatsImpl : NetMessage<CCSUsrMsg_DeepStats>, CCSUsrMsg_DeepStats
{
  public CCSUsrMsg_DeepStatsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CMsgGCCStrike15_ClientDeepStats Stats
  { get => new CMsgGCCStrike15_ClientDeepStatsImpl(NativeNetMessages.GetNestedMessage(Address, "stats"), false); }

}
