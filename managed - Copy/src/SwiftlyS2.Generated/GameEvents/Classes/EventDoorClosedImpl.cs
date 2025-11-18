using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "door_closed"
/// </summary>
internal class EventDoorClosedImpl : GameEvent<EventDoorClosed>, EventDoorClosed
{

  public EventDoorClosedImpl(nint address) : base(address)
  {
  }

  // Who closed the door
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // Who closed the door
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // Who closed the door
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // Who closed the door
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public int EntIndex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }
}
