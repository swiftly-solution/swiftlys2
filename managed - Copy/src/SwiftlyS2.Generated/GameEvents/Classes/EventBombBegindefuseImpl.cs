using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bomb_begindefuse"
/// </summary>
internal class EventBombBegindefuseImpl : GameEvent<EventBombBegindefuse>, EventBombBegindefuse
{

  public EventBombBegindefuseImpl(nint address) : base(address)
  {
  }

  // player who is defusing
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who is defusing
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who is defusing
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who is defusing
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public bool HasKit
  { get => Accessor.GetBool("haskit"); set => Accessor.SetBool("haskit", value); }
}
