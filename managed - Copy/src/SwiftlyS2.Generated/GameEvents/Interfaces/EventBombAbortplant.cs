using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_abortplant"
/// </summary>
public interface EventBombAbortplant : IGameEvent<EventBombAbortplant> {

  static EventBombAbortplant IGameEvent<EventBombAbortplant>.Create(nint address) => new EventBombAbortplantImpl(address);

  static string IGameEvent<EventBombAbortplant>.GetName() => "bomb_abortplant";

  static uint IGameEvent<EventBombAbortplant>.GetHash() => 0x7F1DB601u;
  /// <summary>
  /// player who is planting the bomb
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who is planting the bomb
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who is planting the bomb
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who is planting the bomb
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
