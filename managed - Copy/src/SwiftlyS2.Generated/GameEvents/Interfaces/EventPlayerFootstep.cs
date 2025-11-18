using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_footstep"
/// </summary>
public interface EventPlayerFootstep : IGameEvent<EventPlayerFootstep> {

  static EventPlayerFootstep IGameEvent<EventPlayerFootstep>.Create(nint address) => new EventPlayerFootstepImpl(address);

  static string IGameEvent<EventPlayerFootstep>.GetName() => "player_footstep";

  static uint IGameEvent<EventPlayerFootstep>.GetHash() => 0x5EA9530Bu;
  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// <br/>
  /// type: player_pawn
  /// </summary>
  int UserId { get; set; }

}
