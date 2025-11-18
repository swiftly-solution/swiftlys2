using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_rescued_all"
/// </summary>
public interface EventHostageRescuedAll : IGameEvent<EventHostageRescuedAll>
{

    static EventHostageRescuedAll IGameEvent<EventHostageRescuedAll>.Create( nint address ) => new EventHostageRescuedAllImpl(address);

    static string IGameEvent<EventHostageRescuedAll>.GetName() => "hostage_rescued_all";

    static uint IGameEvent<EventHostageRescuedAll>.GetHash() => 0x9A8C08CEu;
}
