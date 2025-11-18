
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDataGCCStrike15_v2_TournamentGroupTeam : ITypedProtobuf<CDataGCCStrike15_v2_TournamentGroupTeam>
{
  static CDataGCCStrike15_v2_TournamentGroupTeam ITypedProtobuf<CDataGCCStrike15_v2_TournamentGroupTeam>.Wrap(nint handle, bool isManuallyAllocated) => new CDataGCCStrike15_v2_TournamentGroupTeamImpl(handle, isManuallyAllocated);


  public int TeamId { get; set; }


  public int Score { get; set; }


  public bool Correctpick { get; set; }

}
