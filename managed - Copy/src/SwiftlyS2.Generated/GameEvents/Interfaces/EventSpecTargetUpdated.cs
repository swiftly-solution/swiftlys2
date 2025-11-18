using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "spec_target_updated"
/// </summary>
public interface EventSpecTargetUpdated : IGameEvent<EventSpecTargetUpdated> {

  static EventSpecTargetUpdated IGameEvent<EventSpecTargetUpdated>.Create(nint address) => new EventSpecTargetUpdatedImpl(address);

  static string IGameEvent<EventSpecTargetUpdated>.GetName() => "spec_target_updated";

  static uint IGameEvent<EventSpecTargetUpdated>.GetHash() => 0x89A1984Au;
  /// <summary>
  /// spectating player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerController UserIdController { get; }

  /// <summary>
  /// spectating player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  CCSPlayerPawn UserIdPawn { get; }


  // spectating player
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }
  /// <summary>
  /// spectating player
  /// <br/>
  /// type: player_controller_and_pawn
  /// </summary>
  int UserId { get; set; }

  /// <summary>
  /// ehandle of the target
  /// <br/>
  /// type: ehandle
  /// </summary>
  nint Target { get; set; }

}
