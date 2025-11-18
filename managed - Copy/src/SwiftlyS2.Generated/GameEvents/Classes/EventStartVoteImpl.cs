using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "start_vote"
/// </summary>
internal class EventStartVoteImpl : GameEvent<EventStartVote>, EventStartVote
{

  public EventStartVoteImpl(nint address) : base(address)
  {
  }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public byte Type
  { get => (byte)Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }

  public short VoteParameter
  { get => (short)Accessor.GetInt32("vote_parameter"); set => Accessor.SetInt32("vote_parameter", value); }
}
