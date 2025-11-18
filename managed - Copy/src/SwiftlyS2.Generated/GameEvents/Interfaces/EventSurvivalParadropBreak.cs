using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "survival_paradrop_break"
/// </summary>
public interface EventSurvivalParadropBreak : IGameEvent<EventSurvivalParadropBreak> {

  static EventSurvivalParadropBreak IGameEvent<EventSurvivalParadropBreak>.Create(nint address) => new EventSurvivalParadropBreakImpl(address);

  static string IGameEvent<EventSurvivalParadropBreak>.GetName() => "survival_paradrop_break";

  static uint IGameEvent<EventSurvivalParadropBreak>.GetHash() => 0xEBA7AD7Bu;
  /// <summary>
  /// type: short
  /// </summary>
  short EntityID { get; set; }

}
