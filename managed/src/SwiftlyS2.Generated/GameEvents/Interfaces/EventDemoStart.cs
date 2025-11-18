using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "demo_start"
/// </summary>
public interface EventDemoStart : IGameEvent<EventDemoStart>
{

    static EventDemoStart IGameEvent<EventDemoStart>.Create( nint address ) => new EventDemoStartImpl(address);

    static string IGameEvent<EventDemoStart>.GetName() => "demo_start";

    static uint IGameEvent<EventDemoStart>.GetHash() => 0x068617A9u;
}
