using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bomb_dropped"
/// </summary>
internal class EventBombDroppedImpl : GameEvent<EventBombDropped>, EventBombDropped
{

  public EventBombDroppedImpl(nint address) : base(address)
  {
  }

  // player who dropped the bomb
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // player who dropped the bomb
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // player who dropped the bomb
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // player who dropped the bomb
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public int EntIndex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }
}
