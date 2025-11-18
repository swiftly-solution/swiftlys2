using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_begindefuse"
/// </summary>
public interface EventBombBegindefuse : IGameEvent<EventBombBegindefuse> {

  static EventBombBegindefuse IGameEvent<EventBombBegindefuse>.Create(nint address) => new EventBombBegindefuseImpl(address);

  static string IGameEvent<EventBombBegindefuse>.GetName() => "bomb_begindefuse";

  static uint IGameEvent<EventBombBegindefuse>.GetHash() => 0xC3C7D299u;
  /// <summary>
  /// player who is defusing
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who is defusing
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who is defusing
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who is defusing
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// type: bool
  /// </summary>
  bool HasKit { get; set; }

}
