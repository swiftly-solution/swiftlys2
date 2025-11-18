using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_prev_next_spectator"
/// </summary>
public interface EventCsPrevNextSpectator : IGameEvent<EventCsPrevNextSpectator> {

  static EventCsPrevNextSpectator IGameEvent<EventCsPrevNextSpectator>.Create(nint address) => new EventCsPrevNextSpectatorImpl(address);

  static string IGameEvent<EventCsPrevNextSpectator>.GetName() => "cs_prev_next_spectator";

  static uint IGameEvent<EventCsPrevNextSpectator>.GetHash() => 0x532CC8E5u;
  /// <summary>
  /// type: bool
  /// </summary>
  bool Next { get; set; }

}
