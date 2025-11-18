using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cs_prev_next_spectator"
/// </summary>
internal class EventCsPrevNextSpectatorImpl : GameEvent<EventCsPrevNextSpectator>, EventCsPrevNextSpectator
{

  public EventCsPrevNextSpectatorImpl(nint address) : base(address)
  {
  }

  public bool Next
  { get => Accessor.GetBool("next"); set => Accessor.SetBool("next", value); }
}
