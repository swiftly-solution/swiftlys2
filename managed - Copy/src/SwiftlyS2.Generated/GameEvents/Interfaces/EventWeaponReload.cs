using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "weapon_reload"
/// </summary>
public interface EventWeaponReload : IGameEvent<EventWeaponReload> {

  static EventWeaponReload IGameEvent<EventWeaponReload>.Create(nint address) => new EventWeaponReloadImpl(address);

  static string IGameEvent<EventWeaponReload>.GetName() => "weapon_reload";

  static uint IGameEvent<EventWeaponReload>.GetHash() => 0x387E603Fu;
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
