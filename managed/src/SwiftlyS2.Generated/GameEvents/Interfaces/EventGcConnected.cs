using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "gc_connected"
/// </summary>
public interface EventGcConnected : IGameEvent<EventGcConnected>
{

    static EventGcConnected IGameEvent<EventGcConnected>.Create( nint address ) => new EventGcConnectedImpl(address);

    static string IGameEvent<EventGcConnected>.GetName() => "gc_connected";

    static uint IGameEvent<EventGcConnected>.GetHash() => 0xAEFB8477u;
}
