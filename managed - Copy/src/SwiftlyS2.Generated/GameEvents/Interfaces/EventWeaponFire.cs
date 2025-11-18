using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "weapon_fire"
/// </summary>
public interface EventWeaponFire : IGameEvent<EventWeaponFire> {

  static EventWeaponFire IGameEvent<EventWeaponFire>.Create(nint address) => new EventWeaponFireImpl(address);

  static string IGameEvent<EventWeaponFire>.GetName() => "weapon_fire";

  static uint IGameEvent<EventWeaponFire>.GetHash() => 0x78A2D0FEu;
  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// weapon name used
  /// <br/>
  /// type: string
  /// </summary>
  string Weapon { get; set; }

  /// <summary>
  /// is weapon silenced
  /// <br/>
  /// type: bool
  /// </summary>
  bool Silenced { get; set; }

}
