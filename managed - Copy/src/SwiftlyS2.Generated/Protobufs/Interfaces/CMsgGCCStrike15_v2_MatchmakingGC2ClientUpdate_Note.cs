
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_NoteImpl(handle, isManuallyAllocated);


  public int Type { get; set; }


  public int RegionId { get; set; }


  public float RegionR { get; set; }


  public float Distance { get; set; }

}
