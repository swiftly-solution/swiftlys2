using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_officially_ended"
/// </summary>
internal class EventRoundOfficiallyEndedImpl : GameEvent<EventRoundOfficiallyEnded>, EventRoundOfficiallyEnded
{

  public EventRoundOfficiallyEndedImpl(nint address) : base(address)
  {
  }
}
