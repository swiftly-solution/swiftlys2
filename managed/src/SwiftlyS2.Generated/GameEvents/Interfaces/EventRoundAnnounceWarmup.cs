using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_announce_warmup"
/// </summary>
public interface EventRoundAnnounceWarmup : IGameEvent<EventRoundAnnounceWarmup>
{

    static EventRoundAnnounceWarmup IGameEvent<EventRoundAnnounceWarmup>.Create( nint address ) => new EventRoundAnnounceWarmupImpl(address);

    static string IGameEvent<EventRoundAnnounceWarmup>.GetName() => "round_announce_warmup";

    static uint IGameEvent<EventRoundAnnounceWarmup>.GetHash() => 0x352EEA30u;
}
