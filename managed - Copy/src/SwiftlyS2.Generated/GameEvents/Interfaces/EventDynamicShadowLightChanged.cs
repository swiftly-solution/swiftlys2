using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "dynamic_shadow_light_changed"
/// </summary>
public interface EventDynamicShadowLightChanged : IGameEvent<EventDynamicShadowLightChanged> {

  static EventDynamicShadowLightChanged IGameEvent<EventDynamicShadowLightChanged>.Create(nint address) => new EventDynamicShadowLightChangedImpl(address);

  static string IGameEvent<EventDynamicShadowLightChanged>.GetName() => "dynamic_shadow_light_changed";

  static uint IGameEvent<EventDynamicShadowLightChanged>.GetHash() => 0x3FC4330Bu;
}
