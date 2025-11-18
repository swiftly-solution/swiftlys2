using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "survival_no_respawns_final"
/// </summary>
public interface EventSurvivalNoRespawnsFinal : IGameEvent<EventSurvivalNoRespawnsFinal> {

  static EventSurvivalNoRespawnsFinal IGameEvent<EventSurvivalNoRespawnsFinal>.Create(nint address) => new EventSurvivalNoRespawnsFinalImpl(address);

  static string IGameEvent<EventSurvivalNoRespawnsFinal>.GetName() => "survival_no_respawns_final";

  static uint IGameEvent<EventSurvivalNoRespawnsFinal>.GetHash() => 0xE88046CAu;
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

}
