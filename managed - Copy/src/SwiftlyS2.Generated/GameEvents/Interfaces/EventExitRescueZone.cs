using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "exit_rescue_zone"
/// </summary>
public interface EventExitRescueZone : IGameEvent<EventExitRescueZone> {

  static EventExitRescueZone IGameEvent<EventExitRescueZone>.Create(nint address) => new EventExitRescueZoneImpl(address);

  static string IGameEvent<EventExitRescueZone>.GetName() => "exit_rescue_zone";

  static uint IGameEvent<EventExitRescueZone>.GetHash() => 0xEC6242D2u;
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

}
