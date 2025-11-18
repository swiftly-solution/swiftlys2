using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_hurt"
/// </summary>
public interface EventPlayerHurt : IGameEvent<EventPlayerHurt> {

  static EventPlayerHurt IGameEvent<EventPlayerHurt>.Create(nint address) => new EventPlayerHurtImpl(address);

  static string IGameEvent<EventPlayerHurt>.GetName() => "player_hurt";

  static uint IGameEvent<EventPlayerHurt>.GetHash() => 0x1B30DDF0u;
  /// <summary>
  /// player who was hurt
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who was hurt
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who was hurt
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who was hurt
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// player who attacked
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int Attacker { get; set; }

  /// <summary>
  /// remaining health points
  /// <br/>
  /// type: byte
  /// </summary>
  byte Health { get; set; }

  /// <summary>
  /// remaining armor points
  /// <br/>
  /// type: byte
  /// </summary>
  byte Armor { get; set; }

  /// <summary>
  /// weapon name attacker used, if not the world
  /// <br/>
  /// type: string
  /// </summary>
  string Weapon { get; set; }

  /// <summary>
  /// damage done to health
  /// <br/>
  /// type: short
  /// </summary>
  short DmgHealth { get; set; }

  /// <summary>
  /// damage done to armor
  /// <br/>
  /// type: byte
  /// </summary>
  byte DmgArmor { get; set; }

  /// <summary>
  /// hitgroup that was damaged
  /// <br/>
  /// type: byte
  /// </summary>
  byte HitGroup { get; set; }

}
