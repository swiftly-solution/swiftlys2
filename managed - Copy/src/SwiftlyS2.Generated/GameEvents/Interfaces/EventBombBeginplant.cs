using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_beginplant"
/// </summary>
public interface EventBombBeginplant : IGameEvent<EventBombBeginplant> {

  static EventBombBeginplant IGameEvent<EventBombBeginplant>.Create(nint address) => new EventBombBeginplantImpl(address);

  static string IGameEvent<EventBombBeginplant>.GetName() => "bomb_beginplant";

  static uint IGameEvent<EventBombBeginplant>.GetHash() => 0x7DD410C4u;
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
