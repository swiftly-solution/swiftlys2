
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_TournamentGroup : ITypedProtobuf<CDataGCCStrike15_v2_TournamentGroup>
{
  static CDataGCCStrike15_v2_TournamentGroup ITypedProtobuf<CDataGCCStrike15_v2_TournamentGroup>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_TournamentGroupImpl(handle, isManuallyAllocated);


  public uint Groupid { get; set; }


  public string Name { get; set; }


  public string Desc { get; set; }


  public uint PicksDeprecated { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroupTeam> Teams { get; }


  public IProtobufRepeatedFieldValueType<int> StageIds { get; }


  public uint Picklockuntiltime { get; set; }


  public uint Pickableteams { get; set; }


  public uint PointsPerPick { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroup_Picks> Picks { get; }

}
