using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cart_updated"
/// </summary>
internal class EventCartUpdatedImpl : GameEvent<EventCartUpdated>, EventCartUpdated
{

  public EventCartUpdatedImpl(nint address) : base(address)
  {
  }
}
