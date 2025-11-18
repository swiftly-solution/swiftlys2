using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_equip"
/// </summary>
public interface EventItemEquip : IGameEvent<EventItemEquip> {

  static EventItemEquip IGameEvent<EventItemEquip>.Create(nint address) => new EventItemEquipImpl(address);

  static string IGameEvent<EventItemEquip>.GetName() => "item_equip";

  static uint IGameEvent<EventItemEquip>.GetHash() => 0x3D5F333Du;
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
  /// either a weapon such as 'tmp' or 'hegrenade', or an item such as 'nvgs'
  /// <br/>
  /// type: string
  /// </summary>
  string Item { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int DefIndex { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool CanZoom { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool HasSilencer { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool IsSilenced { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool HasTracers { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short WepType { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool IsPainted { get; set; }

}
