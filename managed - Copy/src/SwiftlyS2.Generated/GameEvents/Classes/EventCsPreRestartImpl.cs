using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cs_pre_restart"
/// </summary>
internal class EventCsPreRestartImpl : GameEvent<EventCsPreRestart>, EventCsPreRestart
{

  public EventCsPreRestartImpl(nint address) : base(address)
  {
  }
}
