using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bomb_pickup"
/// </summary>
internal class EventBombPickupImpl : GameEvent<EventBombPickup>, EventBombPickup
{

  public EventBombPickupImpl(nint address) : base(address)
  {
  }

  // player pawn who picked up the bomb
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player pawn who picked up the bomb
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player pawn who picked up the bomb
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player pawn who picked up the bomb
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }
}
