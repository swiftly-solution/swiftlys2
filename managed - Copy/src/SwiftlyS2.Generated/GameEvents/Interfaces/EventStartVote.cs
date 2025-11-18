using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "start_vote"
/// </summary>
public interface EventStartVote : IGameEvent<EventStartVote> {

  static EventStartVote IGameEvent<EventStartVote>.Create(nint address) => new EventStartVoteImpl(address);

  static string IGameEvent<EventStartVote>.GetName() => "start_vote";

  static uint IGameEvent<EventStartVote>.GetHash() => 0x637C08B4u;
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_controller
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// type: byte
  /// </summary>
  byte Type { get; set; }

  /// <summary>
  /// type: short
  /// </summary>
  short VoteParameter { get; set; }

}
