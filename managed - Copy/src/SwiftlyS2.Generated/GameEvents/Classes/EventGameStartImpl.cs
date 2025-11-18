using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "game_start"
/// a new game starts
/// </summary>
internal class EventGameStartImpl : GameEvent<EventGameStart>, EventGameStart
{

  public EventGameStartImpl(nint address) : base(address)
  {
  }

  // max round
  public int RoundsLimit
  { get => Accessor.GetInt32("roundslimit"); set => Accessor.SetInt32("roundslimit", value); }

  // time limit
  public int TimeLimit
  { get => Accessor.GetInt32("timelimit"); set => Accessor.SetInt32("timelimit", value); }

  // frag limit
  public int FragLimit
  { get => Accessor.GetInt32("fraglimit"); set => Accessor.SetInt32("fraglimit", value); }

  // round objective
  public string Objective
  { get => Accessor.GetString("objective"); set => Accessor.SetString("objective", value); }
}
