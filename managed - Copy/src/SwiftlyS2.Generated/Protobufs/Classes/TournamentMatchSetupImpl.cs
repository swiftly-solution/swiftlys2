
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class TournamentMatchSetupImpl : TypedProtobuf<TournamentMatchSetup>, TournamentMatchSetup
{
  public TournamentMatchSetupImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EventId
  { get => Accessor.GetInt32("event_id"); set => Accessor.SetInt32("event_id", value); }


  public int TeamIdCt
  { get => Accessor.GetInt32("team_id_ct"); set => Accessor.SetInt32("team_id_ct", value); }


  public int TeamIdT
  { get => Accessor.GetInt32("team_id_t"); set => Accessor.SetInt32("team_id_t", value); }


  public int EventStageId
  { get => Accessor.GetInt32("event_stage_id"); set => Accessor.SetInt32("event_stage_id", value); }

}
