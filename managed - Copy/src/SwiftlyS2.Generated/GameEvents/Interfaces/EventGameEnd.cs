using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "game_end"
/// a game ended
/// </summary>
public interface EventGameEnd : IGameEvent<EventGameEnd> {

  static EventGameEnd IGameEvent<EventGameEnd>.Create(nint address) => new EventGameEndImpl(address);

  static string IGameEvent<EventGameEnd>.GetName() => "game_end";

  static uint IGameEvent<EventGameEnd>.GetHash() => 0x17FCFCCDu;
  /// <summary>
  /// winner team/user id
  /// <br/>
  /// type: byte
  /// </summary>
  byte Winner { get; set; }

}
