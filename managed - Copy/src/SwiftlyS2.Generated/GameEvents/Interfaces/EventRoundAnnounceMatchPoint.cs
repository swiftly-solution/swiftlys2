using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_announce_match_point"
/// </summary>
public interface EventRoundAnnounceMatchPoint : IGameEvent<EventRoundAnnounceMatchPoint> {

  static EventRoundAnnounceMatchPoint IGameEvent<EventRoundAnnounceMatchPoint>.Create(nint address) => new EventRoundAnnounceMatchPointImpl(address);

  static string IGameEvent<EventRoundAnnounceMatchPoint>.GetName() => "round_announce_match_point";

  static uint IGameEvent<EventRoundAnnounceMatchPoint>.GetHash() => 0xE22B8B5Cu;
}
