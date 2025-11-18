using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "server_message"
/// a generic server message
/// </summary>
public interface EventServerMessage : IGameEvent<EventServerMessage> {

  static EventServerMessage IGameEvent<EventServerMessage>.Create(nint address) => new EventServerMessageImpl(address);

  static string IGameEvent<EventServerMessage>.GetName() => "server_message";

  static uint IGameEvent<EventServerMessage>.GetHash() => 0x29575F36u;
  /// <summary>
  /// the message text
  /// <br/>
  /// type: string
  /// </summary>
  string Text { get; set; }

}
