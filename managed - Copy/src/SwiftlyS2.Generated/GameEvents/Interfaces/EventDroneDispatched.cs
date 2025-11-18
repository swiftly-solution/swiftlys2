using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "drone_dispatched"
/// </summary>
public interface EventDroneDispatched : IGameEvent<EventDroneDispatched> {

  static EventDroneDispatched IGameEvent<EventDroneDispatched>.Create(nint address) => new EventDroneDispatchedImpl(address);

  static string IGameEvent<EventDroneDispatched>.GetName() => "drone_dispatched";

  static uint IGameEvent<EventDroneDispatched>.GetHash() => 0x4491A405u;
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short Priority { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short DroneDispatched { get; set; }

}
