
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPreMatchInfoDataImpl : TypedProtobuf<CPreMatchInfoData>, CPreMatchInfoData
{
  public CPreMatchInfoDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int PredictionsPct
  { get => Accessor.GetInt32("predictions_pct"); set => Accessor.SetInt32("predictions_pct", value); }


  public CDataGCCStrike15_v2_TournamentMatchDraft Draft
  { get => new CDataGCCStrike15_v2_TournamentMatchDraftImpl(NativeNetMessages.GetNestedMessage(Address, "draft"), false); }


  public IProtobufRepeatedFieldSubMessageType<CPreMatchInfoData_TeamStats> Stats
  { get => new ProtobufRepeatedFieldSubMessageType<CPreMatchInfoData_TeamStats>(Accessor, "stats"); }


  public IProtobufRepeatedFieldValueType<int> Wins
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "wins"); }

}
