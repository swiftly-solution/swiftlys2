using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_init"
/// sent when a new game is started
/// </summary>
internal class EventGameInitImpl : GameEvent<EventGameInit>, EventGameInit
{

  public EventGameInitImpl(nint address) : base(address)
  {
  }
}
