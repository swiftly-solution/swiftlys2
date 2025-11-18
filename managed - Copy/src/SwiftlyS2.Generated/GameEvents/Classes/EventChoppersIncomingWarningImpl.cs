using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "choppers_incoming_warning"
/// </summary>
internal class EventChoppersIncomingWarningImpl : GameEvent<EventChoppersIncomingWarning>, EventChoppersIncomingWarning
{

  public EventChoppersIncomingWarningImpl(nint address) : base(address)
  {
  }

  public bool Global
  { get => Accessor.GetBool("global"); set => Accessor.SetBool("global", value); }
}
