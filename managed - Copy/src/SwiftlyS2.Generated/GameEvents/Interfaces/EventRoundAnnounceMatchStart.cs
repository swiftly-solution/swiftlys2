using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_announce_match_start"
/// </summary>
public interface EventRoundAnnounceMatchStart : IGameEvent<EventRoundAnnounceMatchStart> {

  static EventRoundAnnounceMatchStart IGameEvent<EventRoundAnnounceMatchStart>.Create(nint address) => new EventRoundAnnounceMatchStartImpl(address);

  static string IGameEvent<EventRoundAnnounceMatchStart>.GetName() => "round_announce_match_start";

  static uint IGameEvent<EventRoundAnnounceMatchStart>.GetHash() => 0xACF8A0B2u;
}
