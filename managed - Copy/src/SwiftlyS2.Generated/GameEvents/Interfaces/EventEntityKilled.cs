using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "entity_killed"
/// </summary>
public interface EventEntityKilled : IGameEvent<EventEntityKilled> {

  static EventEntityKilled IGameEvent<EventEntityKilled>.Create(nint address) => new EventEntityKilledImpl(address);

  static string IGameEvent<EventEntityKilled>.GetName() => "entity_killed";

  static uint IGameEvent<EventEntityKilled>.GetHash() => 0x6B63D08Eu;
  /// <summary>
  /// type: long
  /// </summary>
  int EntindexKilled { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int EntindexAttacker { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int EntindexInflictor { get; set; }

  /// <summary>
  /// type: long
  /// </summary>
  int DamageBits { get; set; }

}
