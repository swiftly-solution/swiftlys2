using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cs_game_disconnected"
/// </summary>
internal class EventCsGameDisconnectedImpl : GameEvent<EventCsGameDisconnected>, EventCsGameDisconnected
{

  public EventCsGameDisconnectedImpl(nint address) : base(address)
  {
  }
}
