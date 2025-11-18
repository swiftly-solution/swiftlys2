using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "gg_killed_enemy"
/// </summary>
public interface EventGgKilledEnemy : IGameEvent<EventGgKilledEnemy> {

  static EventGgKilledEnemy IGameEvent<EventGgKilledEnemy>.Create(nint address) => new EventGgKilledEnemyImpl(address);

  static string IGameEvent<EventGgKilledEnemy>.GetName() => "gg_killed_enemy";

  static uint IGameEvent<EventGgKilledEnemy>.GetHash() => 0x85DB35E2u;
  /// <summary>
  /// user ID who died
  /// <br/>
  /// type: player_controller
  /// </summary>
  int VictimID { get; set; }

  /// <summary>
  /// user ID who killed
  /// <br/>
  /// type: player_controller
  /// </summary>
  int AttackerID { get; set; }

  /// <summary>
  /// did killer dominate victim with this kill
  /// <br/>
  /// type: short
  /// </summary>
  short Dominated { get; set; }

  /// <summary>
  /// did killer get revenge on victim with this kill
  /// <br/>
  /// type: short
  /// </summary>
  short Revenge { get; set; }

  /// <summary>
  /// did killer kill with a bonus weapon?
  /// <br/>
  /// type: bool
  /// </summary>
  bool Bonus { get; set; }

}
