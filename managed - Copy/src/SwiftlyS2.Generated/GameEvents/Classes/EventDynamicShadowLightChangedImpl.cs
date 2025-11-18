using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "dynamic_shadow_light_changed"
/// </summary>
internal class EventDynamicShadowLightChangedImpl : GameEvent<EventDynamicShadowLightChanged>, EventDynamicShadowLightChanged
{

  public EventDynamicShadowLightChangedImpl(nint address) : base(address)
  {
  }
}
