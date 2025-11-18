using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "weapon_zoom_rifle"
/// </summary>
public interface EventWeaponZoomRifle : IGameEvent<EventWeaponZoomRifle> {

  static EventWeaponZoomRifle IGameEvent<EventWeaponZoomRifle>.Create(nint address) => new EventWeaponZoomRifleImpl(address);

  static string IGameEvent<EventWeaponZoomRifle>.GetName() => "weapon_zoom_rifle";

  static uint IGameEvent<EventWeaponZoomRifle>.GetHash() => 0x4B7652E4u;
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
