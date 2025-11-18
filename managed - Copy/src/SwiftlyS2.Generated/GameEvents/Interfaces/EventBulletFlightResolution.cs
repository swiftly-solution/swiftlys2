using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bullet_flight_resolution"
/// </summary>
public interface EventBulletFlightResolution : IGameEvent<EventBulletFlightResolution> {

  static EventBulletFlightResolution IGameEvent<EventBulletFlightResolution>.Create(nint address) => new EventBulletFlightResolutionImpl(address);

  static string IGameEvent<EventBulletFlightResolution>.GetName() => "bullet_flight_resolution";

  static uint IGameEvent<EventBulletFlightResolution>.GetHash() => 0xB39BC4E7u;
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

}
