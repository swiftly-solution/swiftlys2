
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStats : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStats>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStats ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStatsImpl(handle, isManuallyAllocated);


  public uint GsLocationId { get; set; }


  public uint DataCenterId { get; set; }


  public uint NumLockedIn { get; set; }


  public uint NumFoundNearby { get; set; }


  public uint NoteLevel { get; set; }

}
