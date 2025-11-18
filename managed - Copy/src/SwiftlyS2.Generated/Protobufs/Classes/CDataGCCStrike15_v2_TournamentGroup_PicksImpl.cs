
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentGroup_PicksImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentGroup_Picks>, CDataGCCStrike15_v2_TournamentGroup_Picks
{
  public CDataGCCStrike15_v2_TournamentGroup_PicksImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<int> Pickids
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "pickids"); }

}
