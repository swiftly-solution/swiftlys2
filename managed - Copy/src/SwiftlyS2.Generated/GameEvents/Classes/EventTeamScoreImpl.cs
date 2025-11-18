using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "team_score"
/// team score changed
/// </summary>
internal class EventTeamScoreImpl : GameEvent<EventTeamScore>, EventTeamScore
{

  public EventTeamScoreImpl(nint address) : base(address)
  {
  }

  // team id
  public byte TeamID
  { get => (byte)Accessor.GetInt32("teamid"); set => Accessor.SetInt32("teamid", value); }

  // total team score
  public short Score
  { get => (short)Accessor.GetInt32("score"); set => Accessor.SetInt32("score", value); }
}
