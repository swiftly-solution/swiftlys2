using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "client_disconnect"
/// </summary>
internal class EventClientDisconnectImpl : GameEvent<EventClientDisconnect>, EventClientDisconnect
{

  public EventClientDisconnectImpl(nint address) : base(address)
  {
  }
}
