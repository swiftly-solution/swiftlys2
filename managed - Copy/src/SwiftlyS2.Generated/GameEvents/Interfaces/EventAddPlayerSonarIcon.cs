using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "add_player_sonar_icon"
/// </summary>
public interface EventAddPlayerSonarIcon : IGameEvent<EventAddPlayerSonarIcon> {

  static EventAddPlayerSonarIcon IGameEvent<EventAddPlayerSonarIcon>.Create(nint address) => new EventAddPlayerSonarIconImpl(address);

  static string IGameEvent<EventAddPlayerSonarIcon>.GetName() => "add_player_sonar_icon";

  static uint IGameEvent<EventAddPlayerSonarIcon>.GetHash() => 0x7B807538u;
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
  /// type: float
  /// </summary>
  float PosX { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float PosY { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float PosZ { get; set; }

}
