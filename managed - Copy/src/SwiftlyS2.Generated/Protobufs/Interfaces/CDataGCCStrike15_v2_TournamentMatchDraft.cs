
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_TournamentMatchDraft : ITypedProtobuf<CDataGCCStrike15_v2_TournamentMatchDraft>
{
  static CDataGCCStrike15_v2_TournamentMatchDraft ITypedProtobuf<CDataGCCStrike15_v2_TournamentMatchDraft>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_TournamentMatchDraftImpl(handle, isManuallyAllocated);


  public int EventId { get; set; }


  public int EventStageId { get; set; }


  public int TeamId0 { get; set; }


  public int TeamId1 { get; set; }


  public int MapsCount { get; set; }


  public int MapsCurrent { get; set; }


  public int TeamIdStart { get; set; }


  public int TeamIdVeto1 { get; set; }


  public int TeamIdPickn { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentMatchDraft_Entry> Drafts { get; }


  public IProtobufRepeatedFieldValueType<int> VoteMapid0 { get; }


  public IProtobufRepeatedFieldValueType<int> VoteMapid1 { get; }


  public IProtobufRepeatedFieldValueType<int> VoteMapid2 { get; }


  public IProtobufRepeatedFieldValueType<int> VoteMapid3 { get; }


  public IProtobufRepeatedFieldValueType<int> VoteMapid4 { get; }


  public IProtobufRepeatedFieldValueType<int> VoteMapid5 { get; }


  public IProtobufRepeatedFieldValueType<int> VoteStartingSide { get; }


  public int VotePhase { get; set; }


  public float VotePhaseStart { get; set; }


  public float VotePhaseLength { get; set; }

}
