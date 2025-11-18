using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_hintmessage"
/// </summary>
public interface EventPlayerHintmessage : IGameEvent<EventPlayerHintmessage> {

  static EventPlayerHintmessage IGameEvent<EventPlayerHintmessage>.Create(nint address) => new EventPlayerHintmessageImpl(address);

  static string IGameEvent<EventPlayerHintmessage>.GetName() => "player_hintmessage";

  static uint IGameEvent<EventPlayerHintmessage>.GetHash() => 0xD756F227u;
  /// <summary>
  /// localizable string of a hint
  /// <br/>
  /// type: string
  /// </summary>
  string HintMessage { get; set; }

}
