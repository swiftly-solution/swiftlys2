
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientCommendPlayer : ITypedProtobuf<CMsgGCCStrike15_v2_ClientCommendPlayer>
{
  static CMsgGCCStrike15_v2_ClientCommendPlayer ITypedProtobuf<CMsgGCCStrike15_v2_ClientCommendPlayer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientCommendPlayerImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public ulong MatchId { get; set; }


  public PlayerCommendationInfo Commendation { get; }


  public uint Tokens { get; set; }

}
