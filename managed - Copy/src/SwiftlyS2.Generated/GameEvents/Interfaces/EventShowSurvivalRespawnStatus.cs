using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "show_survival_respawn_status"
/// </summary>
public interface EventShowSurvivalRespawnStatus : IGameEvent<EventShowSurvivalRespawnStatus> {

  static EventShowSurvivalRespawnStatus IGameEvent<EventShowSurvivalRespawnStatus>.Create(nint address) => new EventShowSurvivalRespawnStatusImpl(address);

  static string IGameEvent<EventShowSurvivalRespawnStatus>.GetName() => "show_survival_respawn_status";

  static uint IGameEvent<EventShowSurvivalRespawnStatus>.GetHash() => 0xAF60FAAFu;
  /// <summary>
  /// type: string
  /// </summary>
  string LocToken { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int Duration { get; set; }

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
