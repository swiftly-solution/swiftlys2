using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_activate"
/// </summary>
internal class EventPlayerActivateImpl : GameEvent<EventPlayerActivate>, EventPlayerActivate
{

  public EventPlayerActivateImpl(nint address) : base(address)
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
}
