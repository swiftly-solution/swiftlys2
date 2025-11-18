
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentMatchDraft_EntryImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentMatchDraft_Entry>, CDataGCCStrike15_v2_TournamentMatchDraft_Entry
{
  public CDataGCCStrike15_v2_TournamentMatchDraft_EntryImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Mapid
  { get => Accessor.GetInt32("mapid"); set => Accessor.SetInt32("mapid", value); }


  public int TeamIdCt
  { get => Accessor.GetInt32("team_id_ct"); set => Accessor.SetInt32("team_id_ct", value); }

}
