using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_spawned"
/// </summary>
public interface EventPlayerSpawned : IGameEvent<EventPlayerSpawned> {

  static EventPlayerSpawned IGameEvent<EventPlayerSpawned>.Create(nint address) => new EventPlayerSpawnedImpl(address);

  static string IGameEvent<EventPlayerSpawned>.GetName() => "player_spawned";

  static uint IGameEvent<EventPlayerSpawned>.GetHash() => 0x7DC35E81u;
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
  /// true if restart is pending
  /// <br/>
  /// type: bool
  /// </summary>
  bool InRestart { get; set; }

}
