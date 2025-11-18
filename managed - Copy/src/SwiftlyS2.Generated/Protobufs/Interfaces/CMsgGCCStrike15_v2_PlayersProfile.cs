
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_PlayersProfile : ITypedProtobuf<CMsgGCCStrike15_v2_PlayersProfile>
{
  static CMsgGCCStrike15_v2_PlayersProfile ITypedProtobuf<CMsgGCCStrike15_v2_PlayersProfile>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_PlayersProfileImpl(handle, isManuallyAllocated);


  public uint RequestId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientHello> AccountProfiles { get; }

}
