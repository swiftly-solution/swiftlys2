using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "team_info"
/// info about team
/// </summary>
internal class EventTeamInfoImpl : GameEvent<EventTeamInfo>, EventTeamInfo
{

  public EventTeamInfoImpl(nint address) : base(address)
  {
  }

  // unique team id
  public byte TeamID
  { get => (byte)Accessor.GetInt32("teamid"); set => Accessor.SetInt32("teamid", value); }

  // team name eg "Team Blue"
  public string Teamname
  { get => Accessor.GetString("teamname"); set => Accessor.SetString("teamname", value); }
}
