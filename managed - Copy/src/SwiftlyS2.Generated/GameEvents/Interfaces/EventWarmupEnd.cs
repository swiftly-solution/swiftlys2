using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "warmup_end"
/// </summary>
public interface EventWarmupEnd : IGameEvent<EventWarmupEnd> {

  static EventWarmupEnd IGameEvent<EventWarmupEnd>.Create(nint address) => new EventWarmupEndImpl(address);

  static string IGameEvent<EventWarmupEnd>.GetName() => "warmup_end";

  static uint IGameEvent<EventWarmupEnd>.GetHash() => 0xD874EAEBu;
}
