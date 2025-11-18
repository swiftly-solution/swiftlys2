using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_exploded"
/// </summary>
public interface EventBombExploded : IGameEvent<EventBombExploded> {

  static EventBombExploded IGameEvent<EventBombExploded>.Create(nint address) => new EventBombExplodedImpl(address);

  static string IGameEvent<EventBombExploded>.GetName() => "bomb_exploded";

  static uint IGameEvent<EventBombExploded>.GetHash() => 0x9C543261u;
  /// <summary>
  /// player who planted the bomb
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who planted the bomb
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who planted the bomb
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who planted the bomb
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// bombsite index
  /// <br/>
  /// type: short
  /// </summary>
  short Site { get; set; }

}
