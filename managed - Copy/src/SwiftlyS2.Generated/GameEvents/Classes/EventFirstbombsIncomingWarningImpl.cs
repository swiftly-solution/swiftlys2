using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "firstbombs_incoming_warning"
/// </summary>
internal class EventFirstbombsIncomingWarningImpl : GameEvent<EventFirstbombsIncomingWarning>, EventFirstbombsIncomingWarning
{

  public EventFirstbombsIncomingWarningImpl(nint address) : base(address)
  {
  }

  public bool Global
  { get => Accessor.GetBool("global"); set => Accessor.SetBool("global", value); }
}
