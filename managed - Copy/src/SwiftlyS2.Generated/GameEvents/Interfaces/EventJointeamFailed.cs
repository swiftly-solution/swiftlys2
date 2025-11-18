using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "jointeam_failed"
/// </summary>
public interface EventJointeamFailed : IGameEvent<EventJointeamFailed> {

  static EventJointeamFailed IGameEvent<EventJointeamFailed>.Create(nint address) => new EventJointeamFailedImpl(address);

  static string IGameEvent<EventJointeamFailed>.GetName() => "jointeam_failed";

  static uint IGameEvent<EventJointeamFailed>.GetHash() => 0xCAAD4F84u;
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
  /// 0 = team_full
  /// <br/>
  /// type: byte
  /// </summary>
  byte Reason { get; set; }

}
