using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "enable_restart_voting"
/// </summary>
internal class EventEnableRestartVotingImpl : GameEvent<EventEnableRestartVoting>, EventEnableRestartVoting
{

  public EventEnableRestartVotingImpl(nint address) : base(address)
  {
  }

  public bool Enable
  { get => Accessor.GetBool("enable"); set => Accessor.SetBool("enable", value); }
}
