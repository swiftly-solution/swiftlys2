using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ammo_refill"
/// </summary>
public interface EventAmmoRefill : IGameEvent<EventAmmoRefill> {

  static EventAmmoRefill IGameEvent<EventAmmoRefill>.Create(nint address) => new EventAmmoRefillImpl(address);

  static string IGameEvent<EventAmmoRefill>.GetName() => "ammo_refill";

  static uint IGameEvent<EventAmmoRefill>.GetHash() => 0x65179124u;
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
  /// type: bool
  /// </summary>
  bool Success { get; set; }

}
