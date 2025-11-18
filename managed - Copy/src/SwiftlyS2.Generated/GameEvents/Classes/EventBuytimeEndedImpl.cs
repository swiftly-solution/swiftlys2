using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "buytime_ended"
/// </summary>
internal class EventBuytimeEndedImpl : GameEvent<EventBuytimeEnded>, EventBuytimeEnded
{

  public EventBuytimeEndedImpl(nint address) : base(address)
  {
  }
}
