using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "inspect_weapon"
/// </summary>
public interface EventInspectWeapon : IGameEvent<EventInspectWeapon> {

  static EventInspectWeapon IGameEvent<EventInspectWeapon>.Create(nint address) => new EventInspectWeaponImpl(address);

  static string IGameEvent<EventInspectWeapon>.GetName() => "inspect_weapon";

  static uint IGameEvent<EventInspectWeapon>.GetHash() => 0x211A0C2Cu;
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

}
