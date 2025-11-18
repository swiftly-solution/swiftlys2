using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "server_pre_shutdown"
/// server is about to be shut down
/// </summary>
internal class EventServerPreShutdownImpl : GameEvent<EventServerPreShutdown>, EventServerPreShutdown
{

  public EventServerPreShutdownImpl(nint address) : base(address)
  {
  }

  // reason why server is about to be shut down
  public string Reason
  { get => Accessor.GetString("reason"); set => Accessor.SetString("reason", value); }
}
