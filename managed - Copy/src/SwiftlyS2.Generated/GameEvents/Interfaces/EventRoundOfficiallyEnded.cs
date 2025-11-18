using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_officially_ended"
/// </summary>
public interface EventRoundOfficiallyEnded : IGameEvent<EventRoundOfficiallyEnded> {

  static EventRoundOfficiallyEnded IGameEvent<EventRoundOfficiallyEnded>.Create(nint address) => new EventRoundOfficiallyEndedImpl(address);

  static string IGameEvent<EventRoundOfficiallyEnded>.GetName() => "round_officially_ended";

  static uint IGameEvent<EventRoundOfficiallyEnded>.GetHash() => 0x62F43919u;
}
