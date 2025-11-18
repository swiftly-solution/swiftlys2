using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "game_message"
/// a message send by game logic to everyone
/// </summary>
public interface EventGameMessage : IGameEvent<EventGameMessage> {

  static EventGameMessage IGameEvent<EventGameMessage>.Create(nint address) => new EventGameMessageImpl(address);

  static string IGameEvent<EventGameMessage>.GetName() => "game_message";

  static uint IGameEvent<EventGameMessage>.GetHash() => 0xEA7638FFu;
  /// <summary>
  /// 0 = console, 1 = HUD
  /// <br/>
  /// type: byte
  /// </summary>
  byte Target { get; set; }

  /// <summary>
  /// the message text
  /// <br/>
  /// type: string
  /// </summary>
  string Text { get; set; }

}
