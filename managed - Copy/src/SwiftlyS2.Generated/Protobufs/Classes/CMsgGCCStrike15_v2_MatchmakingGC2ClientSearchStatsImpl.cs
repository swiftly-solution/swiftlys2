
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStatsImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStats>, CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStats
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ClientSearchStatsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint GsLocationId
  { get => Accessor.GetUInt32("gs_location_id"); set => Accessor.SetUInt32("gs_location_id", value); }


  public uint DataCenterId
  { get => Accessor.GetUInt32("data_center_id"); set => Accessor.SetUInt32("data_center_id", value); }


  public uint NumLockedIn
  { get => Accessor.GetUInt32("num_locked_in"); set => Accessor.SetUInt32("num_locked_in", value); }


  public uint NumFoundNearby
  { get => Accessor.GetUInt32("num_found_nearby"); set => Accessor.SetUInt32("num_found_nearby", value); }


  public uint NoteLevel
  { get => Accessor.GetUInt32("note_level"); set => Accessor.SetUInt32("note_level", value); }

}
