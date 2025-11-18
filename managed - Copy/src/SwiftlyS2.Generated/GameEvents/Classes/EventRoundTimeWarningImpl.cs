using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_time_warning"
/// </summary>
internal class EventRoundTimeWarningImpl : GameEvent<EventRoundTimeWarning>, EventRoundTimeWarning
{

  public EventRoundTimeWarningImpl(nint address) : base(address)
  {
  }
}
