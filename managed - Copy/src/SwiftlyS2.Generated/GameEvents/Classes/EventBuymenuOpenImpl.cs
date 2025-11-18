using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "buymenu_open"
/// </summary>
internal class EventBuymenuOpenImpl : GameEvent<EventBuymenuOpen>, EventBuymenuOpen
{

  public EventBuymenuOpenImpl(nint address) : base(address)
  {
  }
}
