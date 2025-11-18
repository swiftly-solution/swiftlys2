
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class TournamentTeamImpl : TypedProtobuf<TournamentTeam>, TournamentTeam
{
  public TournamentTeamImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TeamId
  { get => Accessor.GetInt32("team_id"); set => Accessor.SetInt32("team_id", value); }


  public string TeamTag
  { get => Accessor.GetString("team_tag"); set => Accessor.SetString("team_tag", value); }


  public string TeamFlag
  { get => Accessor.GetString("team_flag"); set => Accessor.SetString("team_flag", value); }


  public string TeamName
  { get => Accessor.GetString("team_name"); set => Accessor.SetString("team_name", value); }


  public IProtobufRepeatedFieldSubMessageType<TournamentPlayer> Players
  { get => new ProtobufRepeatedFieldSubMessageType<TournamentPlayer>(Accessor, "players"); }

}
