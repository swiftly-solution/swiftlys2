using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "match_end_conditions"
/// </summary>
public interface EventMatchEndConditions : IGameEvent<EventMatchEndConditions> {

  static EventMatchEndConditions IGameEvent<EventMatchEndConditions>.Create(nint address) => new EventMatchEndConditionsImpl(address);

  static string IGameEvent<EventMatchEndConditions>.GetName() => "match_end_conditions";

  static uint IGameEvent<EventMatchEndConditions>.GetHash() => 0x036AAC37u;
  /// <summary>
  /// type: long
  /// </summary>
  int FragS { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int MaxRounds { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int WinRounds { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int Time { get; set; }

}
