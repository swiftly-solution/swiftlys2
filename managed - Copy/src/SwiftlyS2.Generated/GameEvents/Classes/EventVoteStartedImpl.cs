using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vote_started"
/// </summary>
internal class EventVoteStartedImpl : GameEvent<EventVoteStarted>, EventVoteStarted
{

  public EventVoteStartedImpl(nint address) : base(address)
  {
  }

  public string Issue
  { get => Accessor.GetString("issue"); set => Accessor.SetString("issue", value); }

  public string Param1
  { get => Accessor.GetString("param1"); set => Accessor.SetString("param1", value); }

  public string VoteData
  { get => Accessor.GetString("votedata"); set => Accessor.SetString("votedata", value); }

  public byte Team
  { get => (byte)Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }

  // entity id of the player who initiated the vote
  public int Initiator
  { get => Accessor.GetInt32("initiator"); set => Accessor.SetInt32("initiator", value); }
}
