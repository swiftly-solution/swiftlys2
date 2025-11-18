using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "buymenu_open"
/// </summary>
public interface EventBuymenuOpen : IGameEvent<EventBuymenuOpen>
{

    static EventBuymenuOpen IGameEvent<EventBuymenuOpen>.Create( nint address ) => new EventBuymenuOpenImpl(address);

    static string IGameEvent<EventBuymenuOpen>.GetName() => "buymenu_open";

    static uint IGameEvent<EventBuymenuOpen>.GetHash() => 0x4DB21865u;
}
