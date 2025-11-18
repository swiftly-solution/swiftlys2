using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "helicopter_grenade_punt_miss"
/// </summary>
public interface EventHelicopterGrenadePuntMiss : IGameEvent<EventHelicopterGrenadePuntMiss>
{

    static EventHelicopterGrenadePuntMiss IGameEvent<EventHelicopterGrenadePuntMiss>.Create( nint address ) => new EventHelicopterGrenadePuntMissImpl(address);

    static string IGameEvent<EventHelicopterGrenadePuntMiss>.GetName() => "helicopter_grenade_punt_miss";

    static uint IGameEvent<EventHelicopterGrenadePuntMiss>.GetHash() => 0xB6DF8460u;
}
