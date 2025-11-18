
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentMatchDraftImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentMatchDraft>, CDataGCCStrike15_v2_TournamentMatchDraft
{
  public CDataGCCStrike15_v2_TournamentMatchDraftImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EventId
  { get => Accessor.GetInt32("event_id"); set => Accessor.SetInt32("event_id", value); }


  public int EventStageId
  { get => Accessor.GetInt32("event_stage_id"); set => Accessor.SetInt32("event_stage_id", value); }


  public int TeamId0
  { get => Accessor.GetInt32("team_id_0"); set => Accessor.SetInt32("team_id_0", value); }


  public int TeamId1
  { get => Accessor.GetInt32("team_id_1"); set => Accessor.SetInt32("team_id_1", value); }


  public int MapsCount
  { get => Accessor.GetInt32("maps_count"); set => Accessor.SetInt32("maps_count", value); }


  public int MapsCurrent
  { get => Accessor.GetInt32("maps_current"); set => Accessor.SetInt32("maps_current", value); }


  public int TeamIdStart
  { get => Accessor.GetInt32("team_id_start"); set => Accessor.SetInt32("team_id_start", value); }


  public int TeamIdVeto1
  { get => Accessor.GetInt32("team_id_veto1"); set => Accessor.SetInt32("team_id_veto1", value); }


  public int TeamIdPickn
  { get => Accessor.GetInt32("team_id_pickn"); set => Accessor.SetInt32("team_id_pickn", value); }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentMatchDraft_Entry> Drafts
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentMatchDraft_Entry>(Accessor, "drafts"); }


  public IProtobufRepeatedFieldValueType<int> VoteMapid0
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_mapid_0"); }


  public IProtobufRepeatedFieldValueType<int> VoteMapid1
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_mapid_1"); }


  public IProtobufRepeatedFieldValueType<int> VoteMapid2
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_mapid_2"); }


  public IProtobufRepeatedFieldValueType<int> VoteMapid3
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_mapid_3"); }


  public IProtobufRepeatedFieldValueType<int> VoteMapid4
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_mapid_4"); }


  public IProtobufRepeatedFieldValueType<int> VoteMapid5
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_mapid_5"); }


  public IProtobufRepeatedFieldValueType<int> VoteStartingSide
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "vote_starting_side"); }


  public int VotePhase
  { get => Accessor.GetInt32("vote_phase"); set => Accessor.SetInt32("vote_phase", value); }


  public float VotePhaseStart
  { get => Accessor.GetFloat("vote_phase_start"); set => Accessor.SetFloat("vote_phase_start", value); }


  public float VotePhaseLength
  { get => Accessor.GetFloat("vote_phase_length"); set => Accessor.SetFloat("vote_phase_length", value); }

}
