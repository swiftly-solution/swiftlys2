using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "add_bullet_hit_marker"
/// </summary>
public interface EventAddBulletHitMarker : IGameEvent<EventAddBulletHitMarker> {

  static EventAddBulletHitMarker IGameEvent<EventAddBulletHitMarker>.Create(nint address) => new EventAddBulletHitMarkerImpl(address);

  static string IGameEvent<EventAddBulletHitMarker>.GetName() => "add_bullet_hit_marker";

  static uint IGameEvent<EventAddBulletHitMarker>.GetHash() => 0x6CB6A2A2u;
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
  short Bone { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short PosX { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short PosY { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short PosZ { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short AngX { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short AngY { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short AngZ { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short StartX { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short StartY { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short StartZ { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool Hit { get; set; }

}
