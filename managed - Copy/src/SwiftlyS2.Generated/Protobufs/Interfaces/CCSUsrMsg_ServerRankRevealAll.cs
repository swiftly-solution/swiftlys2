
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ServerRankRevealAll : ITypedProtobuf<CCSUsrMsg_ServerRankRevealAll>, INetMessage<CCSUsrMsg_ServerRankRevealAll>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ServerRankRevealAll>.MessageId => 350;
  
  static string INetMessage<CCSUsrMsg_ServerRankRevealAll>.MessageName => "CCSUsrMsg_ServerRankRevealAll";

  static CCSUsrMsg_ServerRankRevealAll ITypedProtobuf<CCSUsrMsg_ServerRankRevealAll>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ServerRankRevealAllImpl(handle, isManuallyAllocated);


  public int SecondsTillShutdown { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation { get; }

}
