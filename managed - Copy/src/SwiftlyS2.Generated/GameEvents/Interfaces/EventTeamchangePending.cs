using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "teamchange_pending"
/// </summary>
public interface EventTeamchangePending : IGameEvent<EventTeamchangePending> {

  static EventTeamchangePending IGameEvent<EventTeamchangePending>.Create(nint address) => new EventTeamchangePendingImpl(address);

  static string IGameEvent<EventTeamchangePending>.GetName() => "teamchange_pending";

  static uint IGameEvent<EventTeamchangePending>.GetHash() => 0x53F97450u;
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
  byte ToTeam { get; set; }

}
