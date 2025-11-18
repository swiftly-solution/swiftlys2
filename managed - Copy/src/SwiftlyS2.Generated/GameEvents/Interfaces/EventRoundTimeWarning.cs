using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_time_warning"
/// </summary>
public interface EventRoundTimeWarning : IGameEvent<EventRoundTimeWarning> {

  static EventRoundTimeWarning IGameEvent<EventRoundTimeWarning>.Create(nint address) => new EventRoundTimeWarningImpl(address);

  static string IGameEvent<EventRoundTimeWarning>.GetName() => "round_time_warning";

  static uint IGameEvent<EventRoundTimeWarning>.GetHash() => 0x3E89666Au;
}
