using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hostage_rescued_all"
/// </summary>
internal class EventHostageRescuedAllImpl : GameEvent<EventHostageRescuedAll>, EventHostageRescuedAll
{

  public EventHostageRescuedAllImpl(nint address) : base(address)
  {
  }
}
