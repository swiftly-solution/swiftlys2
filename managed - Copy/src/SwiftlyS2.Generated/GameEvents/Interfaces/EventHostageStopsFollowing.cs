using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_stops_following"
/// </summary>
public interface EventHostageStopsFollowing : IGameEvent<EventHostageStopsFollowing> {

  static EventHostageStopsFollowing IGameEvent<EventHostageStopsFollowing>.Create(nint address) => new EventHostageStopsFollowingImpl(address);

  static string IGameEvent<EventHostageStopsFollowing>.GetName() => "hostage_stops_following";

  static uint IGameEvent<EventHostageStopsFollowing>.GetHash() => 0x63B81600u;
  /// <summary>
  /// player who rescued the hostage
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// player who rescued the hostage
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // player who rescued the hostage
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// player who rescued the hostage
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// hostage entity index
  /// <br/>
  /// type: short
  /// </summary>
  short Hostage { get; set; }

}
