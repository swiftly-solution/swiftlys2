
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPreMatchInfoData : ITypedProtobuf<CPreMatchInfoData>
{
  static CPreMatchInfoData ITypedProtobuf<CPreMatchInfoData>.Wrap(nint handle, bool isManuallyAllocated) => new CPreMatchInfoDataImpl(handle, isManuallyAllocated);


  public int PredictionsPct { get; set; }


  public CDataGCCStrike15_v2_TournamentMatchDraft Draft { get; }


  public IProtobufRepeatedFieldSubMessageType<CPreMatchInfoData_TeamStats> Stats { get; }


  public IProtobufRepeatedFieldValueType<int> Wins { get; }

}
