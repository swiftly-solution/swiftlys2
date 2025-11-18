using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "dronegun_attack"
/// </summary>
public interface EventDronegunAttack : IGameEvent<EventDronegunAttack> {

  static EventDronegunAttack IGameEvent<EventDronegunAttack>.Create(nint address) => new EventDronegunAttackImpl(address);

  static string IGameEvent<EventDronegunAttack>.GetName() => "dronegun_attack";

  static uint IGameEvent<EventDronegunAttack>.GetHash() => 0x3EB09776u;
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

}
