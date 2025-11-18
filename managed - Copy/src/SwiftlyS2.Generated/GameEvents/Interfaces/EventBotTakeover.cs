using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bot_takeover"
/// </summary>
public interface EventBotTakeover : IGameEvent<EventBotTakeover> {

  static EventBotTakeover IGameEvent<EventBotTakeover>.Create(nint address) => new EventBotTakeoverImpl(address);

  static string IGameEvent<EventBotTakeover>.GetName() => "bot_takeover";

  static uint IGameEvent<EventBotTakeover>.GetHash() => 0x6F5C9FCAu;
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
  /// type: player_controller
  /// </summary>
  int BotID { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float P { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float Y { get; set; }

  /// <summary>
  /// type: float
  /// </summary>
  float R { get; set; }

}
