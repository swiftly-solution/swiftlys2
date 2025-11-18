using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "smoke_beacon_paradrop"
/// </summary>
public interface EventSmokeBeaconParadrop : IGameEvent<EventSmokeBeaconParadrop> {

  static EventSmokeBeaconParadrop IGameEvent<EventSmokeBeaconParadrop>.Create(nint address) => new EventSmokeBeaconParadropImpl(address);

  static string IGameEvent<EventSmokeBeaconParadrop>.GetName() => "smoke_beacon_paradrop";

  static uint IGameEvent<EventSmokeBeaconParadrop>.GetHash() => 0xA68BF79Bu;
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
  short ParaDrop { get; set; }

}
