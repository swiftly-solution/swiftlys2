using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vote_changed"
/// </summary>
internal class EventVoteChangedImpl : GameEvent<EventVoteChanged>, EventVoteChanged
{

  public EventVoteChangedImpl(nint address) : base(address)
  {
  }

  public byte YesVotes
  { get => (byte)Accessor.GetInt32("yesVotes"); set => Accessor.SetInt32("yesVotes", value); }

  public byte NoVotes
  { get => (byte)Accessor.GetInt32("noVotes"); set => Accessor.SetInt32("noVotes", value); }

  public byte PotentialVotes
  { get => (byte)Accessor.GetInt32("potentialVotes"); set => Accessor.SetInt32("potentialVotes", value); }

  public byte VoteOption1
  { get => (byte)Accessor.GetInt32("vote_option1"); set => Accessor.SetInt32("vote_option1", value); }

  public byte VoteOption2
  { get => (byte)Accessor.GetInt32("vote_option2"); set => Accessor.SetInt32("vote_option2", value); }

  public byte VoteOption3
  { get => (byte)Accessor.GetInt32("vote_option3"); set => Accessor.SetInt32("vote_option3", value); }

  public byte VoteOption4
  { get => (byte)Accessor.GetInt32("vote_option4"); set => Accessor.SetInt32("vote_option4", value); }

  public byte VoteOption5
  { get => (byte)Accessor.GetInt32("vote_option5"); set => Accessor.SetInt32("vote_option5", value); }
}
