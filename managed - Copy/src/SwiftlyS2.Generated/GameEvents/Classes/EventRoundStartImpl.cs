using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_start"
/// </summary>
internal class EventRoundStartImpl : GameEvent<EventRoundStart>, EventRoundStart
{

  public EventRoundStartImpl(nint address) : base(address)
  {
  }

  // round time limit in seconds
  public int TimeLimit
  { get => Accessor.GetInt32("timelimit"); set => Accessor.SetInt32("timelimit", value); }

  // frag limit in seconds
  public int FragLimit
  { get => Accessor.GetInt32("fraglimit"); set => Accessor.SetInt32("fraglimit", value); }

  // round objective
  public string Objective
  { get => Accessor.GetString("objective"); set => Accessor.SetString("objective", value); }
}
