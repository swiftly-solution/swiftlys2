using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "weaponhud_selection"
/// </summary>
public interface EventWeaponhudSelection : IGameEvent<EventWeaponhudSelection> {

  static EventWeaponhudSelection IGameEvent<EventWeaponhudSelection>.Create(nint address) => new EventWeaponhudSelectionImpl(address);

  static string IGameEvent<EventWeaponhudSelection>.GetName() => "weaponhud_selection";

  static uint IGameEvent<EventWeaponhudSelection>.GetHash() => 0xCA4A1D6Bu;
  /// <summary>
  /// Player who this event applies to
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// Player who this event applies to
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // Player who this event applies to
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// Player who this event applies to
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// EWeaponHudSelectionMode (switch / pickup / drop)
  /// <br/>
  /// type: byte
  /// </summary>
  byte Mode { get; set; }

  /// <summary>
  /// Weapon entity index
  /// <br/>
  /// type: long
  /// </summary>
  int EntIndex { get; set; }

}
