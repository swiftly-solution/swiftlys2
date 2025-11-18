using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "server_shutdown"
/// server shut down
/// </summary>
internal class EventServerShutdownImpl : GameEvent<EventServerShutdown>, EventServerShutdown
{

  public EventServerShutdownImpl(nint address) : base(address)
  {
  }

  // reason why server was shut down
  public string Reason
  { get => Accessor.GetString("reason"); set => Accessor.SetString("reason", value); }
}
