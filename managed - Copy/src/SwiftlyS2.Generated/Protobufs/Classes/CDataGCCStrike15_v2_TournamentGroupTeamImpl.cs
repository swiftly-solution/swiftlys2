
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentGroupTeamImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentGroupTeam>, CDataGCCStrike15_v2_TournamentGroupTeam
{
  public CDataGCCStrike15_v2_TournamentGroupTeamImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TeamId
  { get => Accessor.GetInt32("team_id"); set => Accessor.SetInt32("team_id", value); }


  public int Score
  { get => Accessor.GetInt32("score"); set => Accessor.SetInt32("score", value); }


  public bool Correctpick
  { get => Accessor.GetBool("correctpick"); set => Accessor.SetBool("correctpick", value); }

}
