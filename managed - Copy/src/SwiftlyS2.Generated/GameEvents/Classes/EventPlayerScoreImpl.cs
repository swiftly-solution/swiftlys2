using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_score"
/// players scores changed
/// </summary>
internal class EventPlayerScoreImpl : GameEvent<EventPlayerScore>, EventPlayerScore
{

  public EventPlayerScoreImpl(nint address) : base(address)
  {
  }

  // user ID on server
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // user ID on server
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // user ID on server
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // user ID on server
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // # of kills
  public short Kills
  { get => (short)Accessor.GetInt32("kills"); set => Accessor.SetInt32("kills", value); }

  // # of deaths
  public short Deaths
  { get => (short)Accessor.GetInt32("deaths"); set => Accessor.SetInt32("deaths", value); }

  // total game score
  public short Score
  { get => (short)Accessor.GetInt32("score"); set => Accessor.SetInt32("score", value); }
}
