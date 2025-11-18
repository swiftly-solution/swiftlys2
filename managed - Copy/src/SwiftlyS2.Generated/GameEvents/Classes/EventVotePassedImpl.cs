using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vote_passed"
/// </summary>
internal class EventVotePassedImpl : GameEvent<EventVotePassed>, EventVotePassed
{

  public EventVotePassedImpl(nint address) : base(address)
  {
  }

  public string Details
  { get => Accessor.GetString("details"); set => Accessor.SetString("details", value); }

  public string Param1
  { get => Accessor.GetString("param1"); set => Accessor.SetString("param1", value); }

  public byte Team
  { get => (byte)Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }
}
