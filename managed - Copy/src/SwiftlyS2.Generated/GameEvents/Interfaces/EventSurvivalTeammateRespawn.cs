using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "survival_teammate_respawn"
/// </summary>
public interface EventSurvivalTeammateRespawn : IGameEvent<EventSurvivalTeammateRespawn> {

  static EventSurvivalTeammateRespawn IGameEvent<EventSurvivalTeammateRespawn>.Create(nint address) => new EventSurvivalTeammateRespawnImpl(address);

  static string IGameEvent<EventSurvivalTeammateRespawn>.GetName() => "survival_teammate_respawn";

  static uint IGameEvent<EventSurvivalTeammateRespawn>.GetHash() => 0x558ADEEBu;
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
