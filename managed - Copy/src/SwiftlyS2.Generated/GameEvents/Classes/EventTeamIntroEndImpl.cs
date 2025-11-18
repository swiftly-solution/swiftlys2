using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "team_intro_end"
/// </summary>
internal class EventTeamIntroEndImpl : GameEvent<EventTeamIntroEnd>, EventTeamIntroEnd
{

  public EventTeamIntroEndImpl(nint address) : base(address)
  {
  }
}
