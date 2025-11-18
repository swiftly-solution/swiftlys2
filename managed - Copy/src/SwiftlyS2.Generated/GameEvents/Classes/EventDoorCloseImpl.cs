using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "door_close"
/// </summary>
internal class EventDoorCloseImpl : GameEvent<EventDoorClose>, EventDoorClose
{

  public EventDoorCloseImpl(nint address) : base(address)
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

  // Is the door a checkpoint door
  public bool Checkpoint
  { get => Accessor.GetBool("checkpoint"); set => Accessor.SetBool("checkpoint", value); }
}
