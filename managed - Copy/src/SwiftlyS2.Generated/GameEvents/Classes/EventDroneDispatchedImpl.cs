using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "drone_dispatched"
/// </summary>
internal class EventDroneDispatchedImpl : GameEvent<EventDroneDispatched>, EventDroneDispatched
{

  public EventDroneDispatchedImpl(nint address) : base(address)
  {
  }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public short Priority
  { get => (short)Accessor.GetInt32("priority"); set => Accessor.SetInt32("priority", value); }

  public short DroneDispatched
  { get => (short)Accessor.GetInt32("drone_dispatched"); set => Accessor.SetInt32("drone_dispatched", value); }
}
