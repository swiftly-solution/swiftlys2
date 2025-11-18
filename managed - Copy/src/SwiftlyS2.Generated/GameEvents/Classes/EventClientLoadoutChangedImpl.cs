using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "client_loadout_changed"
/// </summary>
internal class EventClientLoadoutChangedImpl : GameEvent<EventClientLoadoutChanged>, EventClientLoadoutChanged
{

  public EventClientLoadoutChangedImpl(nint address) : base(address)
  {
  }
}
