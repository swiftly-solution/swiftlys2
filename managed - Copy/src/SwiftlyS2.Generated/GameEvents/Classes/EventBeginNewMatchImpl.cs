using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "begin_new_match"
/// </summary>
internal class EventBeginNewMatchImpl : GameEvent<EventBeginNewMatch>, EventBeginNewMatch
{

  public EventBeginNewMatchImpl(nint address) : base(address)
  {
  }
}
