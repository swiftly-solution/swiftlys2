
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_NoteImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note>, CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_NoteImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public int RegionId
  { get => Accessor.GetInt32("region_id"); set => Accessor.SetInt32("region_id", value); }


  public float RegionR
  { get => Accessor.GetFloat("region_r"); set => Accessor.SetFloat("region_r", value); }


  public float Distance
  { get => Accessor.GetFloat("distance"); set => Accessor.SetFloat("distance", value); }

}
