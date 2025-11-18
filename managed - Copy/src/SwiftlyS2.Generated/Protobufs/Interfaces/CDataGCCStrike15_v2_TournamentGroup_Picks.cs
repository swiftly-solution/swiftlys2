
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_TournamentGroup_Picks : ITypedProtobuf<CDataGCCStrike15_v2_TournamentGroup_Picks>
{
  static CDataGCCStrike15_v2_TournamentGroup_Picks ITypedProtobuf<CDataGCCStrike15_v2_TournamentGroup_Picks>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_TournamentGroup_PicksImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<int> Pickids { get; }

}
