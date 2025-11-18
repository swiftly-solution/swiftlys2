using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vote_cast"
/// </summary>
internal class EventVoteCastImpl : GameEvent<EventVoteCast>, EventVoteCast
{

  public EventVoteCastImpl(nint address) : base(address)
  {
  }

  // which option the player voted on
  public byte VoteOption
  { get => (byte)Accessor.GetInt32("vote_option"); set => Accessor.SetInt32("vote_option", value); }

  public short Team
  { get => (short)Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }

  // player who voted
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who voted
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who voted
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who voted
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
