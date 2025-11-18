using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_given_c4"
/// </summary>
internal class EventPlayerGivenC4Impl : GameEvent<EventPlayerGivenC4>, EventPlayerGivenC4
{

  public EventPlayerGivenC4Impl(nint address) : base(address)
  {
  }

  // user ID who received the c4
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // user ID who received the c4
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // user ID who received the c4
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // user ID who received the c4
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
