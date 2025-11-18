using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "demo_stop"
/// </summary>
public interface EventDemoStop : IGameEvent<EventDemoStop> {

  static EventDemoStop IGameEvent<EventDemoStop>.Create(nint address) => new EventDemoStopImpl(address);

  static string IGameEvent<EventDemoStop>.GetName() => "demo_stop";

  static uint IGameEvent<EventDemoStop>.GetHash() => 0xDF8418C3u;
}
